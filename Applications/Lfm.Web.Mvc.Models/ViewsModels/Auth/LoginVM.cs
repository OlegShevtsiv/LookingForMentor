using System.ComponentModel.DataAnnotations;
using Lfm.Core.Common.Web.Models;

namespace Lfm.Web.Mvc.Models.ViewsModels.Auth
{
    public sealed class LoginVM : ReturnUrlModel
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