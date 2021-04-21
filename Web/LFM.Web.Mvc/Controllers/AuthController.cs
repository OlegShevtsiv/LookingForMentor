using System.Threading.Tasks;
using AutoMapper;
using Lfm.Core.Common.Web.Data;
using Lfm.Core.Common.Web.Extensions;
using LFM.Domain.Write.Commands.Auth;
using LFM.Domain.Write.Mediator;
using LFM.Domain.Write.Models;
using Lfm.Web.Mvc.App.Models.ViewModels.Auth;
using Lfm.Web.Mvc.App.SessionAlerts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LFM.Web.Mvc.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IMapper _mapper;

        public AuthController(
            ICommandBus commandBus, 
            IMapper mapper)
        {
            _commandBus = commandBus;
            _mapper = mapper;
        }

        #region Get Pages
        
        [HttpGet("login")]
        public async Task<IActionResult> Login(string returnUrl = Constants.DefaultUrl)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View(new LoginVM{ReturnUrl = returnUrl});
        }

        [HttpGet("register/mentor")]
        public IActionResult RegisterMentor()
        {
            return View(new RegisterMentorVM());
        }
        
        [HttpGet("register/student")]
        public IActionResult RegisterStudent()
        {
            return View(new RegisterStudentVM());
        }
        #endregion

        #region Post Data
        
        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var command = _mapper.Map<LoginUserCommand>(model);
                
                CommandResult loginResult = await _commandBus.ExecuteCommand<LoginUserCommand, CommandResult>(command);
                
                if (loginResult.IsSuccess)
                {
                    this.AlertSuccess(AlertMessages.LoginSuccess);
                    return this.Redirect(model);
                }
            }
            this.AlertError(AlertMessages.LogoutFailed);
            return View(model);
        }

        [HttpPost("register/mentor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterMentor(RegisterMentorVM model)
        {
            if (ModelState.IsValid)
            {
                var command = _mapper.Map<RegisterMentorCommand>(model);
                
                CommandResult loginResult = await _commandBus.ExecuteCommand<RegisterMentorCommand, CommandResult>(command);
                
                if (loginResult.IsSuccess)
                {
                    this.AlertSuccess(AlertMessages.LoginSuccess);
                    return LocalRedirect(Constants.DefaultUrl);
                }
            }
            this.AlertError(AlertMessages.LogoutFailed);
            return View(model);
        }
        
        [HttpPost("register/student")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterStudent(RegisterStudentVM model)
        {
            if (ModelState.IsValid)
            {
                var command = _mapper.Map<RegisterStudentCommand>(model);
                
                CommandResult loginResult = await _commandBus.ExecuteCommand<RegisterStudentCommand, CommandResult>(command);
                
                if (loginResult.IsSuccess)
                {
                    this.AlertSuccess(AlertMessages.LoginSuccess);
                    return LocalRedirect(Constants.DefaultUrl);
                }
            }
            this.AlertError(AlertMessages.LogoutFailed);
            return View(model);
        }

        [Authorize]
        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            LogoutUserCommand command = new LogoutUserCommand();
            CommandResult logoutResult = await _commandBus.ExecuteCommand<LogoutUserCommand, CommandResult>(command);
            
            if (logoutResult.IsSuccess)
                this.AlertSuccess(AlertMessages.LogoutSuccess);
            else
                this.AlertError(AlertMessages.LogoutFailed);
            
            return this.LocalRedirect(Constants.DefaultUrl);
        }
        
        #endregion
    }
}