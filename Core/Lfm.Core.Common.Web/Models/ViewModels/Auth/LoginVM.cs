using System.ComponentModel.DataAnnotations;

namespace Lfm.Core.Common.Web.Models.ViewModels.Auth
{
    public sealed class LoginVM : ReturnUrlVM
    {
        [Required]
        [Display(Name = "Login name")]
        public string LoginName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; } = true;
    }
}