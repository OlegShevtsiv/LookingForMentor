using LFM.Domain.Write.Declarations;

namespace LFM.Domain.Write.Commands.MentorProfile
{
    public class DeleteMentorSubjectCommand : ICommand
    {
        public int MentorId { get; set; }
        
        public int SubjectId { get; set; }
    }
}