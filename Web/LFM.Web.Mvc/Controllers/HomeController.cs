using System.Threading.Tasks;
using LFM.Core.Common.Data;
using LFM.Core.Common.Exceptions;
using LFM.Domain.Read.Providers;
using Lfm.Domain.ReadModels.SearchModels;
using Lfm.Web.Mvc.App.SessionAlerts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LFM.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMentorsProvider _mentorsProvider;

        public HomeController(
            ILogger<HomeController> logger, 
            IMentorsProvider mentorsProvider)
        {
            _logger = logger;
            _mentorsProvider = mentorsProvider;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("looking-for-mentors")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LookingForMentors(MentorsMinSearchModel searchModel)
        {
            var mentors = await _mentorsProvider.LookingForMentors(searchModel);

            if (mentors.TotalCount == 0)
            {
                this.AlertWarning(Messages.DataNotFound);
            }
            
            return View("../Mentors/Mentors", mentors);
        }
        
        public async Task<IActionResult> Privacy()
        {
            return View();
        }
    }
}
