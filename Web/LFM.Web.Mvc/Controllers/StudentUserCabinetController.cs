using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using LFM.Core.Common.Data;
using LFM.Core.Common.Exceptions;
using LFM.DataAccess.DB.Core.Types;
using Lfm.Domain.Common.Extensions;
using LFM.Domain.Read.Providers;
using LFM.Domain.Write.Commands.Order;
using LFM.Domain.Write.Commands.StudentProfile;
using LFM.Domain.Write.Mediator;
using LFM.Domain.Write.Models;
using Lfm.Web.Mvc.App.SessionAlerts;
using Lfm.Web.Mvc.Models.FormModels.UserCabinet.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LFM.Web.Mvc.Controllers
{
    [Authorize(Roles = LfmIdentityRolesNames.Student)]
    [Route("user-cabinet/student")]
    public class StudentUserCabinetController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IStudentProfileProvider _studentProfileProvider;
        private readonly ISubjectsProvider _subjectsProvider;
        private readonly ICommandBus _commandBus;
        
        public StudentUserCabinetController(
            IMapper mapper,
            IStudentProfileProvider studentProfileProvider,
            ICommandBus commandBus, 
            ISubjectsProvider subjectsProvider)
        {
            _mapper = mapper;
            _studentProfileProvider = studentProfileProvider;
            _commandBus = commandBus;
            _subjectsProvider = subjectsProvider;
        }
        
        [HttpGet("lfm-requests")]
        public async Task<IActionResult> LfmRequests()
        {
            var requests = await _studentProfileProvider.GetLfmRequests(User.GetId());
            
            return View("../UserCabinet/Student/LfmRequests", requests);
        }
        
        [HttpGet("lfm-request-details")]
        public async Task<IActionResult> LfmRequestDetails(int orderId)
        {
            var request = await _studentProfileProvider.GetLfmRequestDetails(User.GetId(), orderId);
            
            return View("../UserCabinet/Student/LfmRequestDetails", request);
        }

        [HttpGet("create-lfm-request")]
        public async Task<IActionResult> CreateOrderRequest(int subjectId)
        {
            if (!await _subjectsProvider.IsExists(subjectId))
            {
                this.AlertError(Messages.DataNotFound, "Subject");
                return RedirectToAction("LfmRequests");
            }

            CreateLookingForMentorRequestFormModel model = new CreateLookingForMentorRequestFormModel
            {
                SubjectId = subjectId
            };
            return View("../UserCabinet/Student/CreateOrderRequest", model);
        }
        
        [HttpPost("create-lfm-request")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrderRequest(CreateLookingForMentorRequestFormModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!await _subjectsProvider.IsExists(model.SubjectId))
                    {
                        this.AlertError(Messages.DataNotFound, "Subject");
                        return RedirectToAction("LfmRequests");
                    }
                    
                    var command = _mapper.Map<CreateLookingForMentorRequestFormModel, CreateLookingForMentorRequestCommand>(model);
                    command.StudentId = User.GetId();
                    command.StudentName = User.GetName();
                    command.StudentEmail = User.GetEmail();
                    command.StudentPhoneNumber = User.GetPhoneNumber();

                    var commandResult =
                        await _commandBus.ExecuteCommand<CreateLookingForMentorRequestCommand, CommandResult>(command);

                    if (commandResult.IsSuccess)
                    {
                        this.AlertSuccess(Messages.OrderRequestSuccessful);
                    }
                }
                catch (LfmException exc)
                {
                    this.AlertError(exc.Message);
                }
                catch
                {
                    this.AlertError(Messages.OrderRequestFailed);
                }
            }
            return RedirectToAction("LfmRequests");
        }

        [HttpPost("delete-lfm-request")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOrderRequest([Range(1, int.MaxValue)]int orderId)
        {
            try
            {
                DeleteLookingForMentorRequestCommand command = new DeleteLookingForMentorRequestCommand
                {
                    StudentId = User.GetId(),
                    OrderId = orderId
                };

                var result = await _commandBus.ExecuteCommand<DeleteLookingForMentorRequestCommand, CommandResult>(command);

                if (result.IsSuccess)
                {
                    this.AlertSuccess("Order deleted successfully");
                    return RedirectToAction("LfmRequests");
                }
                this.AlertError("Delete order failed.");
            }
            catch (LfmException exc)
            {
                this.AlertError(exc.Message);
            }
            catch
            {
                this.AlertError(Messages.SystemError);
            }
            return RedirectToAction("LfmRequests");
        }

        [HttpPost("approve-mentor-propose")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveMentorPropose(ApproveMentorProposeFormModel model)
        {
            try
            {
                ApproveMentorProposeCommand command = new ApproveMentorProposeCommand
                {
                    StudentId = User.GetId(),
                    OrderId = model.OrderId,
                    MentorId = model.MentorId
                };

                var result = await _commandBus.ExecuteCommand<ApproveMentorProposeCommand, ApproveMentorProposeResult>(command);

                if (result.IsSuccess)
                {
                    this.AlertSuccess($"Success. {result.MentorName} is you Mentor.");
                    return RedirectToAction("ApprovedOrderDetails", new { model.OrderId });
                }
                this.AlertError("Approving failed.");
            }
            catch (LfmException exc)
            {
                this.AlertError(exc.Message);
            }
            catch
            {
                this.AlertError(Messages.SystemError);
            }
            return RedirectToAction("LfmRequests");
        }
        
        [HttpGet("personal-requests-to-mentors")]
        public async Task<IActionResult> PersonalRequestsToMentors()
        {
            var requests = await _studentProfileProvider.GetPersonalRequestsToMentors(User.GetId());
            
            return View("../UserCabinet/Student/PersonalRequestsToMentors", requests);
        }
        
        [HttpGet("personal-request-to-mentor-details")]
        public async Task<IActionResult> PersonalRequestsToMentorDetails(int orderId)
        {
            var request = await _studentProfileProvider.GetPersonalRequestToMentorDetails(User.GetId(), orderId);
            
            return View("../UserCabinet/Student/PersonalRequestsToMentorDetails", request);
        }
        
        [HttpGet("approved-orders")]
        public async Task<IActionResult> ApprovedOrders()
        {
            var requests = await _studentProfileProvider.GetApprovedRequests(User.GetId());
            
            return View("../UserCabinet/Student/ApprovedOrders", requests);
        }
        
        [HttpGet("approved-order-details")]
        public async Task<IActionResult> ApprovedOrderDetails(int orderId)
        {
            var request = await _studentProfileProvider.GetApprovedRequestDetails(User.GetId(), orderId);
            
            return View("../UserCabinet/Student/ApprovedOrderDetails", request);
        }
    }
}