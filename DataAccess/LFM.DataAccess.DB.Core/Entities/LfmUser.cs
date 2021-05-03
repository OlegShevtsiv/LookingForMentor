using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace LFM.DataAccess.DB.Core.Entities
{
    [Table("LfmUsers")]
    public class LfmUser : IdentityUser<int>
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}