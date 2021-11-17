using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LFM.Core.Common.Data;
using LFM.Core.Common.Exceptions;
using LFM.DataAccess.DB.Core.Context;
using LFM.DataAccess.DB.Core.Entities;
using LFM.Domain.Write.Commands.StudentProfile;
using LFM.Domain.Write.ResultModels;
using LFM.Domain.Write.ToDo;
using Microsoft.EntityFrameworkCore;

namespace LFM.Domain.Write.CommandHandlers.StudentProfile
{
    internal class CreateLookingForMentorRequestCommandHandler :
        BaseNeedsApproveCommandHandler<CreateLookingForMentorRequestCommand, CommandResult>
    {
        private readonly LfmDbContext _context;
        private readonly IMapper _mapper;

        public CreateLookingForMentorRequestCommandHandler(
            LfmDbContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public override ToDoOperationsEnum Operation => ToDoOperationsEnum.CreateLfmRequest;

        public override async Task<CommandResult> ExecuteAsync(CreateLookingForMentorRequestCommand command)
        {
            if (!await CanCreate(command))
            {
                string subjectName = (await _context.Subjects.FirstOrDefaultAsync(s => s.Id == command.SubjectId)).Name;
                throw new LfmException(Messages.OrderRequestAlreadyExist, subjectName);
            }
            
            OrderRequest order = _mapper.Map<OrderRequest>(command);

            _context.OrdersRequests.Add(order);

            await _context.SaveChangesAsync();
            
            return new CommandResult(true);
        }

        private async Task<bool> CanCreate(CreateLookingForMentorRequestCommand command)
        {
            var query = _context.OrdersRequests
                .Where(o => o.StudentId == command.StudentId);
            
            if (await query.CountAsync() >= 10)
                throw new LfmException(Messages.OrderRequestFailed);

            return !await query.AnyAsync(o => o.SubjectId == command.SubjectId);
        }
    }
}