using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LFM.DataAccess.DB.Core.Entities.SubjectEntities;

namespace LFM.DataAccess.DB.Core.Entities.MentorEntities
{
    [Table("MentorsSubjectsInfo")]
    public class MentorsSubjectInfo
    {
        public int Id { get; set; }
        
        public int MentorsProfileId { get; set; }

        [Required]
        public int CostPerHour { get; set; }

        [Required]
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }
        
        public virtual List<MentorsSubjectTag> Tags { get; set; }
    }
}