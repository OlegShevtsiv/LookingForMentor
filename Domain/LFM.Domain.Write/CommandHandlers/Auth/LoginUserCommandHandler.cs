using System.Threading.Tasks;
using LFM.Core.Common.Command;
using LFM.DataAccess.DB.Core.Entities;
using LFM.Domain.Write.Commands.Auth;
using Microsoft.AspNetCore.Identity;

namespace LFM.Domain.Write.CommandHandlers.Auth
{
    internal class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, CommandResult>
    {
        private readonly SignInManager<LfmUser> _signInManager;

        public LoginUserCommandHandler(SignInManager<LfmUser> signInManager)
        {
            _signInManager = signInManager;
        }
        
        public async Task<CommandResult> ExecuteAsync(LoginUserCommand command)
        {
            if (_signInManager.IsSignedIn(_signInManager.Context.User))
            {
                return new CommandResult(false);
            }

            var loginResult = await _signInManager.PasswordSignInAsync(command.LoginName, command.Password, command.RememberMe, lockoutOnFailure: false);
            
            return new CommandResult(loginResult.Succeeded);
        }
    }
}