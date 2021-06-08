using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LFM.Core.Common.Data;
using LFM.Core.Common.Exceptions;
using LFM.DataAccess.DB.Core.Context;
using LFM.DataAccess.DB.Core.Entities;
using LFM.DataAccess.DB.Core.Entities.MentorEntities;
using LFM.Domain.Write.Commands.MentorProfile;
using LFM.Domain.Write.Declarations;
using LFM.Domain.Write.Models;
using Microsoft.EntityFrameworkCore;

namespace LFM.Domain.Write.CommandHandlers.MentorProfile
{
    public class ApprovePersonalOrderCommandHandler : ICommandHandler<ApprovePersonalOrderCommand, CommandResult>
    {
        private readonly LfmDbContext _context;
        private readonly IMapper _mapper;

        public ApprovePersonalOrderCommandHandler(
            LfmDbContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CommandResult> ExecuteAsync(ApprovePersonalOrderCommand command)
        {
            OrderRequest orderRequest = await _context.OrdersRequests
                .Where(o => o.MentorId == command.MentorId && o.Id == command.OrderRequestId)
                .FirstOrDefaultAsync();

            if (orderRequest == null)
                throw new LfmException(Messages.DataNotFound);
            
            ApprovedOrder mentorsOrder = _mapper.Map<OrderRequest, ApprovedOrder>(orderRequest);

            int? costPerHour = (await _context.MentorsSubjectsInfo
                .FirstOrDefaultAsync(s => s.MentorId == command.MentorId && 
                                          s.SubjectId == mentorsOrder.SubjectId))?.CostPerHour;

            if (!costPerHour.HasValue)
                throw new LfmException(Messages.DataNotFound);

            mentorsOrder.CostPerHour = costPerHour.Value;

            _context.ApprovedOrders.Add(mentorsOrder);
            _context.OrdersRequests.Remove(orderRequest);

            await _context.SaveChangesAsync();

            return new CommandResult(true);
        }
    }
}