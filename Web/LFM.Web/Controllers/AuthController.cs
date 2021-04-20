using System.Threading.Tasks;
using LFM.Core.Common.Command;
using Lfm.Core.Common.Web.Extensions;
using Lfm.Core.Common.Web.Models.ViewModels.Auth;
using LFM.Domain.Write.Commands.Auth;
using LFM.Domain.Write.Mediator;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LFM.Web.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly ICommandBus _commandBus;

        public AuthController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        #region Get Pages
        
        [HttpGet("login")]
        public async Task<IActionResult> Login(string returnUrl = "/")
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View(new LoginVM{ReturnUrl = returnUrl});
        }

        [HttpGet("register/mentor/{returnUrl}")]
        public IActionResult RegisterMentor(string returnUrl = "/")
        {
            return View(new RegisterMentorVM{ReturnUrl = returnUrl});
        }
        
        [HttpGet("register/student/{returnUrl}")]
        public IActionResult RegisterStudent(string returnUrl = "/")
        {
            return View(new RegisterStudentVM{ReturnUrl = returnUrl});
        }
        #endregion

        #region Post Data
        
        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                LoginUserCommand command = new LoginUserCommand(model);
                CommandResult loginResult = await _commandBus.ExecuteCommand<LoginUserCommand, CommandResult>(command);
                
                if (loginResult.IsSuccess)
                {
                    this.AlertSuccess("Login successful.");
                    return this.LocalRedirect(model);
                }
            }
            this.AlertError("Invalid login attempt.");
            return View(model);
        }

        [HttpPost("register/mentor")]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterMentor(RegisterMentorVM model)
        {
            return this.LocalRedirect(model);
        }
        
        [HttpPost("register/student")]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterStudent(RegisterStudentVM model)
        {
            return this.LocalRedirect(model);
        }

        [Authorize]
        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            LogoutUserCommand command = new LogoutUserCommand();
            CommandResult logoutResult = await _commandBus.ExecuteCommand<LogoutUserCommand, CommandResult>(command);
            
            if (logoutResult.IsSuccess)
                this.AlertSuccess("Logout successful.");
            else
                this.AlertError("Invalid logout attempt.");
            
            return this.LocalRedirect();
        }
        
        #endregion
    }
}