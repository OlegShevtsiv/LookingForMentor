using LFM.Domain.Write.Declarations;

namespace LFM.Domain.Write.Commands.Auth
{
    public class RegisterMentorCommand : ICommand
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        
        public string MiddleName { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }
    }
}