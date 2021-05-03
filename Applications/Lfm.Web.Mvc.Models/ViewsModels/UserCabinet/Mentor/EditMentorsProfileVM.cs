using System.ComponentModel.DataAnnotations;
using LFM.DataAccess.DB.Core.Types;
using Microsoft.AspNetCore.Http;

namespace Lfm.Web.Mvc.Models.ViewsModels.UserCabinet.Mentor
{
    public class EditMentorsProfileVM
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string  Surname { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string  MiddleName { get; set; }

        public int? ProfileImageId { get; set; }

        public IFormFile ProfileImageFormFile { get; set; }

        [Required]
        [MaxLength(250)]
        public string AboutMe { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string LocationInfo { get; set; }

        [Required]
        public StudyingPlaces? StudyingPlace { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string Education { get; set; }
    }
}