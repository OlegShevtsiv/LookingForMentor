using System;
using LFM.DataAccess.DB.Core.Types;

namespace Lfm.Domain.ReadModels.ReviewModels.MentorProfile
{
    public class MentorPersonalOrdersMinReviewModel
    {
        public int Id { get; set; }
        
        public string SubjectName { get; set; }

        public string TagName { get; set; }

        public string StudentName { get; set; }
        
        public string StudentPhoneNumber { get; set; }
        
        public string StudentEmail { get; set; }
        
        public DateTime CreationDateTime { get; set; }
    }
}