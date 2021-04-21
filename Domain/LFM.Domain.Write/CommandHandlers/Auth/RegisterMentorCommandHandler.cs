using System.Threading.Tasks;
using LFM.Domain.Write.Commands.Auth;
using LFM.Domain.Write.Declarations;
using LFM.Domain.Write.Models;

namespace LFM.Domain.Write.CommandHandlers.Auth
{
    public class RegisterMentorCommandHandler : ICommandHandler<RegisterMentorCommand, CommandResult>
    {
        public Task<CommandResult> ExecuteAsync(RegisterMentorCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}