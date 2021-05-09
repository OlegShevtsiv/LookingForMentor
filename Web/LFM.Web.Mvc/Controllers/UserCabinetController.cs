using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using LFM.Core.Common.Exceptions;
using LFM.DataAccess.DB.Core.Types;
using Lfm.Domain.Common.Extensions;
using LFM.Domain.Read.Providers;
using Lfm.Domain.ReadModels.ReviewModels.MentorProfile;
using LFM.Domain.Write.Commands.MentorProfile;
using LFM.Domain.Write.Mediator;
using LFM.Domain.Write.Models;
using Lfm.Web.Mvc.App.Attributes.Action;
using Lfm.Web.Mvc.App.SessionAlerts;
using Lfm.Web.Mvc.Models.ViewsModels.UserCabinet.Mentor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LFM.Web.Mvc.Controllers
{
    [Route("user-cabinet")]
    public class UserCabinetController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMentorProfileProvider _mentorProfileProvider;
        private readonly IWebHostEnvironment _environment;
        private readonly ICommandBus _commandBus;

        public UserCabinetController(
            IMapper mapper,
            IMentorProfileProvider mentorProfileProvider,
            IWebHostEnvironment env, 
            ICommandBus commandBus)
        {
            _mapper = mapper;
            _mentorProfileProvider = mentorProfileProvider;
            _environment = env;
            _commandBus = commandBus;
        }

        [Authorize(Roles = LfmIdentityRolesNames.Mentor + "," + LfmIdentityRolesNames.Student)]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region Mentor
        
        [Authorize(Roles = LfmIdentityRolesNames.Mentor)]
        [HttpGet("mentor/general-info")]
        public async Task<IActionResult> MentorGeneralInfo()
        {
            var model = await _mentorProfileProvider.GetGeneralInfo<MentorProfilePreviewModel>(User.GetId());
            return MentorView("MentorGeneralInfo", model);
        }
        
        [Authorize(Roles = LfmIdentityRolesNames.Mentor)]
        [HttpGet("mentor/edit-profile")]
        public async Task<IActionResult> EditMentorGeneralInfo()
        {
            var model = await _mentorProfileProvider.GetGeneralInfo<EditMentorsProfileVM>(User.GetId());
            model.Name = User.GetName();
            return MentorView("EditMentorGeneralInfo", model);
        }
        
        [Authorize(Roles = LfmIdentityRolesNames.Mentor)]
        [HttpPost("mentor/edit-profile")]
        //[AlertModelStateErrors]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMentorGeneralInfo(EditMentorsProfileVM model)
        {
            if (ModelState.IsValid)
            {
                var command = _mapper.Map<EditMentorProfileCommand>(model);
                command.MentorId = User.GetId();

                try
                {
                    var result = await _commandBus.ExecuteCommand<EditMentorProfileCommand, CommandResult>(command);

                    if (result.IsSuccess)
                    {
                        this.AlertSuccess("Profile info updated successfully.");
                        return RedirectToAction("MentorGeneralInfo");
                    }
                }
                catch (LfmException exc)
                {
                    this.AlertError(exc.Message);
                }
                catch
                {
                    this.AlertError(Messages.SystemError);
                }
            }
            return MentorView("EditMentorGeneralInfo", model);
        }
        
        [Authorize(Roles = LfmIdentityRolesNames.Mentor)]
        [HttpGet("mentor/subjects-info")]
        public async Task<IActionResult> MentorSubjectsInfo()
        {
            var subjectsInfo = await _mentorProfileProvider.GetSubjectsInfo(User.GetId());
            
            return MentorView("MentorSubjectsInfo", subjectsInfo);
        }

        [Authorize(Roles = LfmIdentityRolesNames.Mentor)]
        [HttpGet("mentor/adding-subject/{subjectId:int}")]
        public async Task<IActionResult> AddingMentorSubject([Required] int subjectId)
        {
            if (!await _mentorProfileProvider.CanAddSubject(User.GetId(), subjectId))
            {
                this.AlertError("Unable to add subject.");
                return RedirectToAction("MentorSubjectsInfo");
            }
            
            return MentorView("AddMentorSubject", new AddMentorsSubjectVM{SubjectId = subjectId});
        }
        
        [Authorize(Roles = LfmIdentityRolesNames.Mentor)]
        [HttpPost("mentor/add-subject")]
        //[AlertModelStateErrors]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMentorSubject(AddMentorsSubjectVM model)
        {
            if (ModelState.IsValid)
            {
                var command = _mapper.Map<AddMentorSubjectCommand>(model);
                command.MentorId = User.GetId();
                try
                {
                    var result = await _commandBus.ExecuteCommand<AddMentorSubjectCommand, CommandResult>(command);

                    if (result.IsSuccess)
                    {
                        this.AlertSuccess("Subject added successfully");
                        return RedirectToAction("MentorSubjectsInfo");
                    }
                }
                catch(LfmException exc)
                {
                    this.AlertError(exc.Message);
                }
                catch
                {
                    this.AlertError(Messages.SystemError);
                }
            }
            return MentorView("AddMentorSubject", model);
        }
        
        [Authorize(Roles = LfmIdentityRolesNames.Mentor)]
        [HttpGet("mentor/editing-subject/{subjectId:int}")]
        public async Task<IActionResult> EditingMentorSubject([Required] int subjectId)
        {
            var subject = await _mentorProfileProvider.GetSubject(User.GetId(), subjectId);
            if (subject == null)
            {
                this.AlertError("Subject not found.");
                return RedirectToAction("MentorSubjectsInfo");
            }
            
            var model = _mapper.Map<EditMentorsSubjectVM>(subject);
            return MentorView("EditMentorSubject", model);
        }
        
        [Authorize(Roles = LfmIdentityRolesNames.Mentor)]
        [HttpPost("mentor/edit-subject")]
        //[AlertModelStateErrors]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMentorSubject(EditMentorsSubjectVM model)
        {
            if (ModelState.IsValid)
            {
                var command = _mapper.Map<EditMentorSubjectCommand>(model);
                command.MentorId = User.GetId();
                try
                {
                    var result = await _commandBus.ExecuteCommand<EditMentorSubjectCommand, CommandResult>(command);

                    if (result.IsSuccess)
                    {
                        this.AlertSuccess("Subject edited successfully");
                        return RedirectToAction("MentorSubjectsInfo");
                    }
                }
                catch(LfmException exc)
                {
                    this.AlertError(exc.Message);
                }
                catch
                {
                    this.AlertError(Messages.SystemError);
                }
            }
            return MentorView("EditMentorSubject", model);
        }

        [Authorize(Roles = LfmIdentityRolesNames.Mentor)]
        [HttpPost("mentor/delete-subject")]
        //[AlertModelStateErrors]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMentorSubject([Required] int subjectId)
        {
            try
            {
                DeleteMentorSubjectCommand command = new DeleteMentorSubjectCommand
                {
                    MentorId = User.GetId(),
                    SubjectId = subjectId
                };
                
                var result = await _commandBus.ExecuteCommand<DeleteMentorSubjectCommand, CommandResult>(command);

                if (result.IsSuccess)
                {
                    this.AlertSuccess("Subject deleted successfully");
                }
            }
            catch(LfmException exc)
            {
                this.AlertError(exc.Message);
            }
            catch
            {
                this.AlertError(Messages.SystemError);
            }
            return RedirectToAction("MentorSubjectsInfo");
        }

        [Authorize(Roles = LfmIdentityRolesNames.Mentor)]
        [HttpGet("mentor/avatar")]
        public async Task<IActionResult> GetMentorAvatar()
        {
            var avatar = await _mentorProfileProvider.GetAvatar(User.GetId());

            if (avatar?.Length > 0)
            {
                return File(avatar, "image/jpeg", $"mentor_avatar");
            }
            return ReturnDefaultMentorAvatar();
        }

        [NonAction]
        private IActionResult ReturnDefaultMentorAvatar()
        {
            Image im = Image.FromFile($"{this._environment.WebRootPath}/images/default-avatar.png");

            MemoryStream ms = new MemoryStream();
            im.Save(ms, im.RawFormat);

            return File(ms.ToArray(), "image/png", "default-avatar.png");
        }

        private IActionResult MentorView(string viewName, object model)
        {
            return View($"Mentor/{viewName}", model);
        }
        
        #endregion
    }
}