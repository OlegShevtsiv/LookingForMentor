using System.Threading.Tasks;
using Lfm.Common.Blazor.App.PageModels;
using LFM.Core.Common.Data;
using LFM.DataAccess.DB.Core.Entities;
using LFM.DataAccess.DB.Core.Types;
using Lfm.Domain.Common.Services.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Lfm.Web.Admin.Blazor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<LfmUser> _signInManager;
        private readonly ILfmRoleManager _roleManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(
            SignInManager<LfmUser> signInManager, 
            ILfmRoleManager roleManager, 
            ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [BindProperty]
        public LoginInputModel Input { get; set; }
        
        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
        

        public Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");
            ReturnUrl = returnUrl;
            
            return Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            
            if (ModelState.IsValid)
            {
                LfmUser user = await _signInManager.UserManager.FindByEmailAsync(Input.Email);
                
                if (user != null)
                {
                    var role = await _roleManager.RetrieveUserRole(user.Id);
                    if (role == LfmIdentityRolesEnum.Admin)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                        
                        if (result.Succeeded)
                        {
                            _logger.LogInformation("User logged in.");
                            return LocalRedirect(returnUrl);
                        }
                        
                        ModelState.AddModelError(string.Empty, Messages.LoginFailed);
                        return Page();
                    }
                }
                
                ModelState.AddModelError(string.Empty, string.Format(Messages.DataNotFound, "??????????????????????"));
                return Page();
            }

            return Page();
        }
    }
}
