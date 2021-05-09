using System.Collections.Generic;
using LFM.Domain.Write.Declarations;

namespace LFM.Domain.Write.Commands.MentorProfile
{
    public class EditMentorSubjectCommand : ICommand
    {
        public int MentorId { get; set; }
        
        public int CostPerHour { get; set; }

        
        public string Description { get; set; }

        public int SubjectId { get; set; }
            
        public List<int> TagIds { get; set; }
    }
}