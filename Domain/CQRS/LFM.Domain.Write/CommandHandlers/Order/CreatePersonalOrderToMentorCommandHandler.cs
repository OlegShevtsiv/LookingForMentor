using System.Threading.Tasks;
using AutoMapper;
using LFM.DataAccess.DB.Core.Context;
using LFM.DataAccess.DB.Core.Entities;
using LFM.DataAccess.DB.Core.Types;
using LFM.Domain.Write.Commands.Order;
using LFM.Domain.Write.Declarations;
using LFM.Domain.Write.Models;
using Microsoft.EntityFrameworkCore;

namespace LFM.Domain.Write.CommandHandlers.Order
{
    public class CreatePersonalOrderToMentorCommandHandler : ICommandHandler<CreatePersonalOrderToMentorCommand, CreatePersonalOrderResult>
    {
        private readonly LfmDbContext _context;
        private readonly IMapper _mapper;

        public CreatePersonalOrderToMentorCommandHandler(
            LfmDbContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreatePersonalOrderResult> ExecuteAsync(CreatePersonalOrderToMentorCommand command)
        {
            if (!await CanCreate(command.MentorId, command.SubjectId))
            {
                return new CreatePersonalOrderResult(false);
            }

            OrderRequest order = _mapper.Map<CreatePersonalOrderToMentorCommand, OrderRequest>(command);

            StudyingPlaces? mentorStudyingPlace =
                (await _context.MentorsProfiles.FirstOrDefaultAsync(m => m.MentorId == command.MentorId)).StudyingPlace;

            if (mentorStudyingPlace.HasValue && mentorStudyingPlace != StudyingPlaces.ONLINE_AND_OFFLINE)
                order.StudyingPlace = mentorStudyingPlace.Value;

            _context.OrdersRequests.Add(order);

            await _context.SaveChangesAsync();

            string mentorName = (await _context.LfmUsers.FirstOrDefaultAsync(m => m.Id == command.MentorId)).Name;

            return new CreatePersonalOrderResult(true) { MentorName = mentorName};
        }

        private async Task<bool> CanCreate(int mentorId, int subjectId)
        {
            bool isOrderExists = await _context.OrdersRequests
                .AnyAsync(p => p.MentorId.HasValue && 
                               p.MentorId.Value == mentorId && 
                               p.SubjectId == subjectId);

            return !isOrderExists;
        }
    }
}