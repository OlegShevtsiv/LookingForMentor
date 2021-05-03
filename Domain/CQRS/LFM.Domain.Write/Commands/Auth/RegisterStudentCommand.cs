using LFM.Domain.Write.Declarations;

namespace LFM.Domain.Write.Commands.Auth
{
    public class RegisterStudentCommand : ICommand
    {
        public string Name { get; set; }
        
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        
        public string Password { get; set; }
    }
}