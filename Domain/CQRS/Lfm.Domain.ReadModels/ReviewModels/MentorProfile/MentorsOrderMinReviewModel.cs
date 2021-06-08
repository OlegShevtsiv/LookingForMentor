using System;

namespace Lfm.Domain.ReadModels.ReviewModels.MentorProfile
{
    public class MentorsOrderMinReviewModel
    {
        public int Id { get; set; }
        
        public string SubjectName { get; set; }

        public string TagName { get; set; }

        public string StudentName { get; set; }
        
        public string StudentPhoneNumber { get; set; }
        
        public string StudentEmail { get; set; }
        
        public DateTime DateTime { get; set; }
    }
}