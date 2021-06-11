using LFM.Domain.Write.Declarations;

namespace LFM.Domain.Write.Commands.StudentProfile
{
    public class DeleteLookingForMentorRequestCommand : ICommand
    {
        public int OrderId { get; set; }
        
        public int StudentId { get; set; }
    }
}