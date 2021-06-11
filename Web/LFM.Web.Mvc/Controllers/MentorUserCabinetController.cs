using System.Collections.Generic;
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
    [Route("user-cabinet/mentor")]
    public class MentorUserCabinetController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMentorProfileProvider _mentorProfileProvider;
        private readonly ICommandBus _commandBus;
        private readonly ISubjectsProvider _subjectsProvider;

        
        public MentorUserCabinetController(
            IMapper mapper,
            IMentorProfileProvider mentorProfileProvider,
            ICommandBus commandBus, 
            ISubjectsProvider subjectsProvider)
        {
            _mapper = mapper;
            _mentorProfileProvider = mentorProfileProvider;
            _commandBus = commandBus;
            _subjectsProvider = subjectsProvider;
        }
        
        
        [HttpGet("general-info")]
        public async Task<IActionResult> GeneralInfo()
        {
            var model = await _mentorProfileProvider.GetGeneralInfo<MentorProfilePreviewModel>(User.GetId());
            return View("../UserCabinet/Mentor/GeneralInfo", model);
        }

        [HttpGet("edit-profile")]
        public async Task<IActionResult> EditGeneralInfo()
        {
            var model = await _mentorProfileProvider.GetGeneralInfo<EditMentorsProfileFormModel>(User.GetId());
            model.Name = User.GetName();
            return View("../UserCabinet/Mentor/EditGeneralInfo", model);
        }

        [HttpPost("edit-profile")]
        //[AlertModelStateErrors]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGeneralInfo(EditMentorsProfileFormModel model)
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
                        return RedirectToAction("GeneralInfo");
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
            return View("../UserCabinet/Mentor/EditGeneralInfo", model);
        }

        [HttpGet("subjects-info")]
        public async Task<IActionResult> SubjectsInfo()
        {
            var subjectsInfo = await _mentorProfileProvider.GetSubjectsInfo(User.GetId());

            return View("../UserCabinet/Mentor/SubjectsInfo", subjectsInfo);
        }

        [HttpGet("adding-subject/{subjectId:int}")]
        public async Task<IActionResult> AddSubject([Required] int subjectId)
        {
            if (!await _mentorProfileProvider.CanAddSubject(User.GetId(), subjectId))
            {
                this.AlertError("Unable to add subject.");
                return RedirectToAction("SubjectsInfo");
            }

            return View("../UserCabinet/Mentor/AddSubject", new AddMentorsSubjectFormModel {SubjectId = subjectId});
        }

        [HttpPost("add-subject")]
        //[AlertModelStateErrors]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMentorSubject(AddMentorsSubjectFormModel model)
        {
            if (ModelState.IsValid)
            {
                if (!await _subjectsProvider.IsExists(model.SubjectId))
                {
                    this.AlertError(Messages.DataNotFound, "Subject");
                    return RedirectToAction("SubjectsInfo");
                }
                
                var command = _mapper.Map<AddMentorSubjectCommand>(model);
                command.MentorId = User.GetId();
                try
                {
                    var result = await _commandBus.ExecuteCommand<AddMentorSubjectCommand, CommandResult>(command);

                    if (result.IsSuccess)
                    {
                        this.AlertSuccess("Subject added successfully");
                        return RedirectToAction("SubjectsInfo");
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

            return View("../UserCabinet/Mentor/AddSubject", model);
        }

        [HttpGet("editing-subject/{subjectId:int}")]
        public async Task<IActionResult> EditSubject([Required] int subjectId)
        {
            var subject = await _mentorProfileProvider.GetSubject(User.GetId(), subjectId);
            if (subject == null)
            {
                this.AlertError("Subject not found.");
                return RedirectToAction("SubjectsInfo");
            }

            var model = _mapper.Map<EditMentorsSubjectFormModel>(subject);
            return View("../UserCabinet/Mentor/EditSubject", model);
        }

        [HttpPost("edit-subject")]
        //[AlertModelStateErrors]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubject(EditMentorsSubjectFormModel model)
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
                        return RedirectToAction("SubjectsInfo");
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
            return View("../UserCabinet/Mentor/EditSubject", model);
        }

        [HttpPost("delete-subject")]
        //[AlertModelStateErrors]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSubject([Required] int subjectId)
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
                    return RedirectToAction("SubjectsInfo");
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
            return RedirectToAction("SubjectsInfo");
        }

        [HttpGet("personal-orders")]
        public async Task<IActionResult> PersonalOrders()
        {
            var data = await _mentorProfileProvider.GetPersonalOrdersRequests(User.GetId());

            return View("../UserCabinet/Mentor/PersonalOrders", data);
        }
        
        [HttpGet("personal-order-details")]
        public async Task<IActionResult> PersonalOrderDetails(int orderId)
        {
            var data = await _mentorProfileProvider.GetPersonalOrderRequestDetails(User.GetId(), orderId);

            return View("../UserCabinet/Mentor/PersonalOrderDetails", data);
        }

        [HttpPost("approve-personal-order")]
        public async Task<IActionResult> ApprovePersonalOrderRequest(int orderId)
        {
            try
            {
                ApprovePersonalOrderCommand command = new ApprovePersonalOrderCommand
                {
                    MentorId = User.GetId(),
                    OrderRequestId = orderId
                };
                
                CommandResult commandResult = await _commandBus.ExecuteCommand<ApprovePersonalOrderCommand, CommandResult>(command);

                if (commandResult.IsSuccess)
                {
                    this.AlertSuccess("Approve personal order successful.");
                    return RedirectToAction("ApprovedOrderDetails", new { orderId });
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
            return RedirectToAction("PersonalOrderDetails", new {orderId });
        }
        
        [HttpPost("reject-personal-order")]
        public async Task<IActionResult> RejectPersonalOrderRequest(int orderId)
        {
            try
            {
                RejectPersonalOrderCommand command = new RejectPersonalOrderCommand
                {
                    MentorId = User.GetId(),
                    OrderRequestId = orderId
                };
                
                CommandResult commandResult = await _commandBus.ExecuteCommand<RejectPersonalOrderCommand, CommandResult>(command);

                if (commandResult.IsSuccess)
                {
                    this.AlertSuccess("Reject personal order successful.");
                    return RedirectToAction("PersonalOrders");
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
            return RedirectToAction("PersonalOrderDetails", new {orderId });
        }
        
        [HttpGet("approved-orders")]
        public async Task<IActionResult> ApprovedOrders()
        {
            var data = await _mentorProfileProvider.GetApprovedOrders(User.GetId());

            return View("../UserCabinet/Mentor/ApprovedOrders", data);
        }
        
        [HttpGet("approved-order-details")]
        public async Task<IActionResult> ApprovedOrderDetails(int orderId)
        {
            var data = await _mentorProfileProvider.GetApprovedOrderDetails(User.GetId(), orderId);

            return View("../UserCabinet/Mentor/ApprovedOrderDetails", data);
        }
        
        [HttpGet("potential-orders")]
        public async Task<IActionResult> PotentialOrders()
        {
            IEnumerable<MentorPotentialOrderReviewModel> data = new List<MentorPotentialOrderReviewModel>();
            try
            {
                data = await _mentorProfileProvider.GetPotentialOrders(User.GetId());
            }
            catch (LfmException exc)
            {
                this.AlertWarning(exc.Message);
            }
            
            return View("../UserCabinet/Mentor/PotentialOrders", data);
        }
        
        [HttpGet("potential-order-details")]
        public async Task<IActionResult> PotentialOrderDetails(int orderId)
        {
            MentorPotentialOrderDetailsReviewModel order;
            try
            {
                order = await _mentorProfileProvider.GetPotentialOrderDetails(User.GetId(), orderId);
            }
            catch (LfmException exc)
            {
                this.AlertError(exc.Message);
                return RedirectToAction("PotentialOrders");
            }

            return View("../UserCabinet/Mentor/PotentialOrderDetails", order);
        }
        
        [HttpPost("potential-order-propose")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PotentialOrderPropose(int orderId)
        {
            try
            {
                InterestOrderCommand command = new InterestOrderCommand
                {
                    MentorId = User.GetId(),
                    OrderId = orderId
                };
                
                CommandResult commandResult = await _commandBus.ExecuteCommand<InterestOrderCommand, CommandResult>(command);

                if (commandResult.IsSuccess)
                {
                    this.AlertSuccess("Your interest was send.");
                    return RedirectToAction("PotentialOrderDetails", new { orderId });
                }
                this.AlertError("Yor interest was not taken into account.");
            }
            catch (LfmException exc)
            {
                this.AlertError(exc.Message);
            }
            catch
            {
                this.AlertError(Messages.SystemError);
            }
            return RedirectToAction("PotentialOrders", new {orderId });
        }
    }
}