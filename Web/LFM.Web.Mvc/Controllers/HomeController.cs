using System.Threading.Tasks;
using LFM.Core.Common.Data;
using Lfm.Core.Common.Web.Configurations;
using LFM.Domain.Read.Providers;
using Lfm.Domain.ReadModels.SearchModels;
using Lfm.Web.Mvc.App.SessionAlerts;
using Lfm.Web.Mvc.App.StaticServices;
using Lfm.Web.Mvc.Models.FormModels.Mentor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LFM.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMentorsProvider _mentorsProvider;
        private readonly AppConfigurations _appConfigs;

        public HomeController(
            ILogger<HomeController> logger, 
            IMentorsProvider mentorsProvider,
            IOptions<AppConfigurations> configOptions)
        {
            _logger = logger;
            _mentorsProvider = mentorsProvider;
            _appConfigs = configOptions.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("looking-for-mentors")]
        public async Task<IActionResult> LookingForMentors([FromQuery]MentorsSearchModel searchModel, int? pageNumber)
        {
            var mentors = await _mentorsProvider.LookingForMentors(searchModel, pageNumber);

            if (mentors.TotalCount == 0)
            {
                this.AlertWarning(Messages.DataNotFound);
            }
            
            CommonStaticService.PushLastSearchMentorsRequest(HttpContext, searchModel);

            MentorPageModel pageModel = new MentorPageModel(mentors, searchModel, _appConfigs.SearchingMentorsPageSize);
            
            return View("../Mentors/Mentors", pageModel);
        }

        public async Task<IActionResult> Privacy()
        {
            return View();
        }
    }
}
