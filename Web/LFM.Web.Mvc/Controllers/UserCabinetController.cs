using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using LFM.DataAccess.DB.Core.Types;
using Lfm.Domain.Common.Extensions;
using LFM.Domain.Read.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LFM.Web.Mvc.Controllers
{
    [Route("user-cabinet")]
    public class UserCabinetController : Controller
    {
        private readonly IMentorProfileProvider _mentorProfileProvider;
        private readonly IWebHostEnvironment _environment;

        public UserCabinetController(
            IMentorProfileProvider mentorProfileProvider, 
            IWebHostEnvironment environment)
        {
            _mentorProfileProvider = mentorProfileProvider;
            _environment = environment;
        }

        [Authorize(Roles = LfmIdentityRolesNames.Mentor + "," + LfmIdentityRolesNames.Student)]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet("mentor/avatar")]
        public async Task<IActionResult> GetMentorAvatar(int? mentorId)
        {
            var avatar = await _mentorProfileProvider.GetAvatar(mentorId ?? User.GetId());

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
    }
}