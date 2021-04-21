using System.Threading.Tasks;
using LFM.DataAccess.DB.Core.Entities;
using LFM.Domain.Write.Commands.Auth;
using LFM.Domain.Write.Declarations;
using LFM.Domain.Write.Models;
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

            var loginResult = await _signInManager.PasswordSignInAsync(command.Email, command.Password, command.RememberMe, lockoutOnFailure: false);
            
            return new CommandResult(loginResult.Succeeded);
        }
    }
}