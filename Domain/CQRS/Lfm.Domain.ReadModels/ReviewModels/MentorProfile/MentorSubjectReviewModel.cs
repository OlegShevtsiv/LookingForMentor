using System.Collections.Generic;

namespace Lfm.Domain.ReadModels.ReviewModels.MentorProfile
{
    public class MentorSubjectReviewModel
    {
        public int CostPerHour { get; set; }
        
        public string Description { get; set; }

        public int SubjectId { get; set; }

        public string SubjectName { get; set; }
        
        public virtual List<MentorsSubjectTag> SelectedTags { get; set; }
        
        public class MentorsSubjectTag
        {
            public int Id { get; set; }

            public string TagName { get; set; }
        }
    }
}