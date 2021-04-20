using LFM.Core.Common.Command;
using Lfm.Core.Common.Web.Models.ViewModels.Auth;

namespace LFM.Domain.Write.Commands.Auth
{
    public class LoginUserCommand : ICommand
    {
        public LoginUserCommand(LoginVM model)
        {
            LoginName = model.LoginName;
            Password = model.Password;
            RememberMe = model.RememberMe;
        }

        public string LoginName { get; }
        
        public string Password { get; }

        public bool RememberMe { get; }
    }
}