using System.Threading.Tasks;
using LFM.Core.Common.Command;
using LFM.DataAccess.DB.Core.Entities;
using LFM.Domain.Write.Commands.Auth;
using Microsoft.AspNetCore.Identity;

namespace LFM.Domain.Write.CommandHandlers.Auth
{
    internal class LogoutUserCommandHandler : ICommandHandler<LogoutUserCommand, CommandResult>
    {
        private readonly SignInManager<LfmUser> _signInManager;
        
        public LogoutUserCommandHandler(SignInManager<LfmUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public async Task<CommandResult> ExecuteAsync(LogoutUserCommand command)
        {
            await _signInManager.SignOutAsync();
            return new CommandResult(true);
        }
    }
}