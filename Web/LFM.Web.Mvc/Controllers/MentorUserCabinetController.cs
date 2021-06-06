using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using LFM.Core.Common.Data;
using LFM.Core.Common.Exceptions;
using LFM.DataAccess.DB.Core.Types;
using Lfm.Domain.Common.Extensions;
using LFM.Domain.Read.Providers;
using Lfm.Domain.ReadModels.ReviewModels.MentorProfile;
using LFM.Domain.Write.Commands.MentorProfile;
using LFM.Domain.Write.Mediator;
using LFM.Domain.Write.Models;
using Lfm.Web.Mvc.App.SessionAlerts;
using Lfm.Web.Mvc.Models.FormModels.UserCabinet.Mentor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LFM.Web.Mvc.Controllers
{
    [Authorize(Roles = LfmIdentityRolesNames.Mentor)]
    [Route("user-cabinet")]
    public class MentorUserCabinetController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMentorProfileProvider _mentorProfileProvider;
        private readonly ICommandBus _commandBus;
        
        public MentorUserCabinetController(
            IMapper mapper,
            IMentorProfileProvider mentorProfileProvider,
            ICommandBus commandBus)
        {
            _mapper = mapper;
            _mentorProfileProvider = mentorProfileProvider;
            _commandBus = commandBus;
        }
        
        
        [HttpGet("mentor/general-info")]
        public async Task<IActionResult> MentorGeneralInfo()
        {
            var model = await _mentorProfileProvider.GetGeneralInfo<MentorProfilePreviewModel>(User.GetId());
            return View("../UserCabinet/Mentor/MentorGeneralInfo", model);
        }

        [HttpGet("mentor/edit-profile")]
        public async Task<IActionResult> EditMentorGeneralInfo()
        {
            var model = await _mentorProfileProvider.GetGeneralInfo<EditMentorsProfileFormModel>(User.GetId());
            model.Name = User.GetName();
            return View("../UserCabinet/Mentor/EditMentorGeneralInfo", model);
        }

        [HttpPost("mentor/edit-profile")]
        //[AlertModelStateErrors]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMentorGeneralInfo(EditMentorsProfileFormModel model)
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
                    this.AlertError("Updating Profile info failed.");
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
            return View("../UserCabinet/Mentor/EditMentorGeneralInfo", model);
        }

        [HttpGet("mentor/subjects-info")]
        public async Task<IActionResult> MentorSubjectsInfo()
        {
            var subjectsInfo = await _mentorProfileProvider.GetSubjectsInfo(User.GetId());

            return View("../UserCabinet/Mentor/MentorSubjectsInfo", subjectsInfo);
        }

        [HttpGet("mentor/adding-subject/{subjectId:int}")]
        public async Task<IActionResult> AddingMentorSubject([Required] int subjectId)
        {
            if (!await _mentorProfileProvider.CanAddSubject(User.GetId(), subjectId))
            {
                this.AlertError("Unable to add subject.");
                return RedirectToAction("MentorSubjectsInfo");
            }

            return View("../UserCabinet/Mentor/AddMentorSubject", new AddMentorsSubjectFormModel {SubjectId = subjectId});
        }

        [HttpPost("mentor/add-subject")]
        //[AlertModelStateErrors]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMentorSubject(AddMentorsSubjectFormModel model)
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
                    this.AlertError("Addition subject failed.");
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

            return View("../UserCabinet/Mentor/AddMentorSubject", model);
        }

        [HttpGet("mentor/editing-subject/{subjectId:int}")]
        public async Task<IActionResult> EditingMentorSubject([Required] int subjectId)
        {
            var subject = await _mentorProfileProvider.GetSubject(User.GetId(), subjectId);
            if (subject == null)
            {
                this.AlertError("Subject not found.");
                return RedirectToAction("MentorSubjectsInfo");
            }

            var model = _mapper.Map<EditMentorsSubjectFormModel>(subject);
            return View("../UserCabinet/Mentor/EditMentorSubject", model);
        }

        [HttpPost("mentor/edit-subject")]
        //[AlertModelStateErrors]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMentorSubject(EditMentorsSubjectFormModel model)
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
                    this.AlertError("Editing subject failed.");
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
            return View("../UserCabinet/Mentor/EditMentorSubject", model);
        }

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
                this.AlertError("Delete subject failed.");
            }
            catch (LfmException exc)
            {
                this.AlertError(exc.Message);
            }
            catch
            {
                this.AlertError(Messages.SystemError);
            }
            return RedirectToAction("MentorSubjectsInfo");
        }

        [HttpGet("mentor/personal-orders")]
        public async Task<IActionResult> MentorPersonalOrders()
        {
            var data = await _mentorProfileProvider.GetPersonalOrders<MentorPersonalOrdersMinReviewModel>(HttpContext.User.GetId());

            return View("../UserCabinet/Mentor/MentorPersonalOrders", data);
        }
        
        [HttpGet("mentor/personal-order-details")]
        public async Task<IActionResult> MentorPersonalOrderDetails(int orderId)
        {
            var data = await _mentorProfileProvider.GetPersonalOrderDetails(HttpContext.User.GetId(), orderId);

            return View("../UserCabinet/Mentor/MentorPersonalOrderDetails", data);
        }

        [HttpPost("mentor/approve-personal-order")]
        public async Task<IActionResult> ApprovePersonalOrderRequest(int orderId)
        {
            try
            {
                ApprovePersonalOrderCommand command = new ApprovePersonalOrderCommand
                {
                    MentorId = HttpContext.User.GetId(),
                    OrderRequestId = orderId
                };
                
                CommandResult commandResult = await _commandBus.ExecuteCommand<ApprovePersonalOrderCommand, CommandResult>(command);

                if (commandResult.IsSuccess)
                {
                    this.AlertSuccess("Approve personal order successful.");
                    return RedirectToAction("MentorPersonalOrders");
                }
                this.AlertError("Approve personal order failed.");
            }
            catch (LfmException exc)
            {
                this.AlertError(exc.Message);
            }
            catch
            {
                this.AlertError(Messages.SystemError);
            }
            return RedirectToAction("MentorPersonalOrderDetails", new {orderId });
        }
        
        [HttpPost("mentor/reject-personal-order")]
        public async Task<IActionResult> RejectPersonalOrderRequest(int orderId)
        {
            try
            {
                RejectPersonalOrderCommand command = new RejectPersonalOrderCommand
                {
                    MentorId = HttpContext.User.GetId(),
                    OrderRequestId = orderId
                };
                
                CommandResult commandResult = await _commandBus.ExecuteCommand<RejectPersonalOrderCommand, CommandResult>(command);

                if (commandResult.IsSuccess)
                {
                    this.AlertSuccess("Reject personal order successful.");
                    return RedirectToAction("MentorPersonalOrders");
                }
                this.AlertError("Reject personal order failed.");
            }
            catch (LfmException exc)
            {
                this.AlertError(exc.Message);
            }
            catch
            {
                this.AlertError(Messages.SystemError);
            }
            return RedirectToAction("MentorPersonalOrderDetails", new {orderId });
        }
    }
}