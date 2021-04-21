using System.Threading.Tasks;
using LFM.Domain.Write.Commands.Auth;
using LFM.Domain.Write.Declarations;
using LFM.Domain.Write.Models;

namespace LFM.Domain.Write.CommandHandlers.Auth
{
    public class RegisterStudentCommandHandler : ICommandHandler<RegisterStudentCommand, CommandResult>
    {
        public Task<CommandResult> ExecuteAsync(RegisterStudentCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}