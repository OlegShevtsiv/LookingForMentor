using System.ComponentModel.DataAnnotations;
using Lfm.Core.Common.Web.Models;

namespace Lfm.Web.Mvc.Models.FormModels.Auth
{
    public sealed class LoginFormModel : ReturnUrlModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; } = true;
    }
}