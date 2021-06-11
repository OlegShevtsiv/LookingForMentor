using LFM.Domain.Write.Declarations;

namespace LFM.Domain.Write.Commands.StudentProfile
{
    public class ApproveMentorProposeCommand : ICommand
    {
        public int OrderId { get; set; }

        public int MentorId { get; set; }

        public int StudentId { get; set; }
    }
}