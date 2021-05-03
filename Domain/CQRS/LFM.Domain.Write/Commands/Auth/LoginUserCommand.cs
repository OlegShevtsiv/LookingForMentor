using LFM.Domain.Write.Declarations;

namespace LFM.Domain.Write.Commands.Auth
{
    public class LoginUserCommand : ICommand
    {
        public string Email { get; set; }
        
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}