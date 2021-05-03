using System.Collections.Generic;
using LFM.DataAccess.DB.Core.Types;

namespace Lfm.Domain.ReadModels.ReviewModels.MentorProfile
{
    public class MentorProfilePreviewModel
    {
        public string  Surname { get; set; }
        
        public string  MiddleName { get; set; }

        public int ProfileImageId { get; set; }
        
        public string AboutMe { get; set; }

        public List<SubjectInfo> SubjectsInfos { get; set; }
        
        public string LocationInfo { get; set; }

        public StudyingPlaces StudyingPlace { get; set; }
        
        public string Education { get; set; }
        
        public class SubjectInfo
        {
            public int CostPerHour { get; set; }
            
            public string SubjectName { get; set; }
            
            public List<TagInfo> Tags { get; set; }
        }
        
        public class TagInfo
        {
            public int Id { get; set; }
            
            public string Name { get; set; }
        }
    }
}