using System.Threading.Tasks;
using AutoMapper;
using LFM.Core.Common.Data;
using LFM.Core.Common.Exceptions;
using LFM.DataAccess.DB.Core.Types;
using Lfm.Domain.Common.Extensions;
using LFM.Domain.Read.Providers;
using LFM.Domain.Write.Commands.Order;
using LFM.Domain.Write.Mediator;
using LFM.Domain.Write.Models;
using Lfm.Web.Mvc.App.SessionAlerts;
using Lfm.Web.Mvc.Models.FormModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LFM.Web.Mvc.Controllers
{
    [Authorize(Roles = LfmIdentityRolesNames.Student)]
    [Route("user-cabinet")]
    public class StudentUserCabinetController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IStudentProfileProvider _studentProfileProvider;
        private readonly ISubjectsProvider _subjectsProvider;
        private readonly ICommandBus _commandBus;
        
        public StudentUserCabinetController(
            IMapper mapper,
            IStudentProfileProvider studentProfileProvider,
            ICommandBus commandBus, ISubjectsProvider subjectsProvider)
        {
            _mapper = mapper;
            _studentProfileProvider = studentProfileProvider;
            _commandBus = commandBus;
            _subjectsProvider = subjectsProvider;
        }
        
        [HttpGet("find-mentors-requests")]
        public async Task<IActionResult> FindMentorsRequests()
        {
            var requests = await _studentProfileProvider.GetFindMentorRequests(User.GetId());
            
            return View("../UserCabinet/Student/FindMentorsRequests", requests);
        }
        
        [HttpGet("find-mentors-request-details")]
        public async Task<IActionResult> FindMentorsRequestDetails()
        {
            var requests = await _studentProfileProvider.GetFindMentorRequests(User.GetId());
            
            return View("../UserCabinet/Student/FindMentorsRequests", requests);
        }

        [HttpGet("create-looking-for-mentor-request")]
        public async Task<IActionResult> CreateOrderRequest(int subjectId)
        {
            if (!await _subjectsProvider.IsExists(subjectId))
            {
                this.AlertError(Messages.DataNotFound, "Subject");
                return RedirectToAction("FindMentorsRequests");
            }

            CreateOrderFormModel model = new CreateOrderFormModel
            {
                SubjectId = subjectId
            };
            return View("../UserCabinet/Student/CreateOrderRequest", model);
        }
        
        [HttpPost("create-looking-for-mentor-request")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrderRequest(CreateOrderFormModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!await _subjectsProvider.IsExists(model.SubjectId))
                    {
                        this.AlertError(Messages.DataNotFound, "Subject");
                        return RedirectToAction("FindMentorsRequests");
                    }
                    
                    var command = _mapper.Map<CreateOrderFormModel, CreateLookingForMentorRequestCommand>(model);
                    command.StudentId = User.GetId();
                    command.StudentName = User.GetName();
                    command.StudentEmail = User.GetEmail();
                    command.StudentPhoneNumber = User.GetPhoneNumber();

                    var commandResult =
                        await _commandBus.ExecuteCommand<CreateLookingForMentorRequestCommand, CommandResult>(command);

                    if (commandResult.IsSuccess)
                    {
                        this.AlertSuccess(Messages.OrderRequestSuccessful);
                        return RedirectToAction("Index", "UserCabinet");
                    }

                    this.AlertSuccess(Messages.OrderRequestFailed);
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
            return View("../UserCabinet/Student/CreateOrderRequest", model);
        }
    }
}