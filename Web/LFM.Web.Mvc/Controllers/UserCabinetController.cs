using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
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
    [Route("userCabinet")]
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

        [Authorize(Roles = LfmIdentityRolesNames.Mentor)]
        [HttpGet("generalInfo")]
        public async Task<IActionResult> MentorGeneralInfo()
        {
            var model = await _mentorProfileProvider.GetGeneralInfo<MentorProfilePreviewModel>(User.GetId());
            return View(model);
        }
        
        [Authorize(Roles = LfmIdentityRolesNames.Mentor)]
        [HttpGet("subjectsInfo")]
        public async Task<IActionResult> MentorSubjectsInfo()
        {
            return View();
        }

        [Authorize(Roles = LfmIdentityRolesNames.Mentor)]
        [HttpGet("editMentorProfile")]
        public async Task<IActionResult> EditMentorGeneralInfo()
        {
            var model = await _mentorProfileProvider.GetGeneralInfo<EditMentorsProfileVM>(User.GetId());
            model.Name = User.GetName();
            return View(model);
        }
        
        [Authorize(Roles = LfmIdentityRolesNames.Mentor)]
        [HttpPost("editMentorProfile")]
        [AlertModelStateErrors]
        public async Task<IActionResult> EditMentorGeneralInfo(EditMentorsProfileVM model)
        {
            if (ModelState.IsValid)
            {
                EditMentorProfileCommand command = _mapper.Map<EditMentorProfileCommand>(model);
                command.MentorId = User.GetId();
                
                if (model.ProfileImageFormFile != null)
                {
                    using var binaryReader = new BinaryReader(model.ProfileImageFormFile.OpenReadStream());
                    command.ProfileImageBytes = binaryReader.ReadBytes((int)model.ProfileImageFormFile.Length);
                }
                
                var result = await _commandBus.ExecuteCommand<EditMentorProfileCommand, CommandResult>(command);

                if (result.IsSuccess)
                {
                    this.AlertSuccess("Profile info updated successfully.");
                    return RedirectToAction("MentorGeneralInfo");
                }
            }
            return View(model);
        }

        [Authorize(Roles = LfmIdentityRolesNames.Mentor)]
        [HttpGet("GetMentorsAvatar")]
        public async Task<IActionResult> GetMentorsAvatar()
        {
            var avatar = await _mentorProfileProvider.GetAvatar(User.GetId());

            if (avatar == null)
            {
                return ReturnDefaultAvatar();
            }

            return File(avatar, "image/jpeg", $"mentor_avatar");
        }

        [NonAction]
        private IActionResult ReturnDefaultAvatar()
        {
            Image im = Image.FromFile($"{this._environment.WebRootPath}/images/default-avatar.png");

            MemoryStream ms = new MemoryStream();
            im.Save(ms, im.RawFormat);

            return File(ms.ToArray(), "image/png", "default-avatar.png");
        }
    }
}