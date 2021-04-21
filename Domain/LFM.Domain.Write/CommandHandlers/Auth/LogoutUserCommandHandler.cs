using System.Threading.Tasks;
using LFM.DataAccess.DB.Core.Entities;
using LFM.Domain.Write.Commands.Auth;
using LFM.Domain.Write.Declarations;
using LFM.Domain.Write.Models;
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