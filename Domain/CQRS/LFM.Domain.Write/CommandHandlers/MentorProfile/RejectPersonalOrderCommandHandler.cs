using System.Linq;
using System.Threading.Tasks;
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
    public class RejectPersonalOrderCommandHandler : ICommandHandler<RejectPersonalOrderCommand, CommandResult>
    {
        private readonly LfmDbContext _context;

        public RejectPersonalOrderCommandHandler(LfmDbContext context)
        {
            _context = context;
        }

        public async Task<CommandResult> ExecuteAsync(RejectPersonalOrderCommand command)
        {
            OrderRequest order = await _context.OrdersRequests
                .Where(o => o.MentorId == command.MentorId && o.Id == command.OrderRequestId)
                .FirstOrDefaultAsync();

            if (order == null)
                throw new LfmException(Messages.DataNotFound);
            
            _context.OrdersRequests.Remove(order);
            await _context.SaveChangesAsync();

            return new CommandResult(true);
        }
    }
}