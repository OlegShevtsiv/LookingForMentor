using System;
using System.Linq;
using System.Threading.Tasks;
using LFM.Core.Common.Data;
using LFM.Core.Common.DataProcessors;
using LFM.Core.Common.Exceptions;
using LFM.DataAccess.DB.Core.Context;
using LFM.DataAccess.DB.Core.Entities;
using LFM.DataAccess.DB.Core.Entities.MentorEntities;
using LFM.DataAccess.DB.Core.Types;
using Lfm.Domain.Common.Caching.User;
using LFM.Domain.Write.Commands.MentorProfile;
using LFM.Domain.Write.Declarations;
using LFM.Domain.Write.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LFM.Domain.Write.CommandHandlers.MentorProfile
{
    internal class EditMentorProfileCommandHandler : ICommandHandler<EditMentorProfileCommand, CommandResult>
    {
        private readonly LfmDbContext _context;
        private readonly SignInManager<LfmUser> _signInManager;
        private readonly IUserCachingService _userCachingService;

        public EditMentorProfileCommandHandler(
            LfmDbContext context, 
            SignInManager<LfmUser> signInManager, 
            IUserCachingService userCachingService)
        {
            _context = context;
            _signInManager = signInManager;
            _userCachingService = userCachingService;
        }

        public async Task<CommandResult> ExecuteAsync(EditMentorProfileCommand command)
        {
            var mentor = await _signInManager.UserManager
                .FindByIdAsync(command.MentorId.ToString());

            if (mentor == null)
                throw new LfmException(Messages.DataNotFound, "User");

            var profile = await _context.MentorsProfiles
                .Include(m => m.ProfileImage)
                .FirstOrDefaultAsync(m => m.MentorId == command.MentorId);

            if (profile == null)
                throw new LfmException(Messages.DataNotFound);

            var isTownExist = await _context.UkrainianTowns.AnyAsync(t => t.Id == command.TownId);
            if (!isTownExist)
            {
                throw new LfmException(Messages.DataNotFound, "Town");
            }

            if (!mentor.Name.Equals(command.Name, StringComparison.InvariantCulture))
            {
                mentor.Name = command.Name;
                var updateResult = await _signInManager.UserManager.UpdateAsync(mentor);
                if (!updateResult.Succeeded)
                {
                    throw new LfmException(updateResult.Errors.FirstOrDefault()?.Description ?? Messages.SystemError);
                }
                await _userCachingService.RemoveUserFromCache();
                await _userCachingService.TryCacheUser((mentor, LfmIdentityRolesEnum.Mentor));
                await _signInManager.RefreshSignInAsync(mentor);
            }
            
            if (HasChangesAndApply(profile, command))
            {
                profile.IsVerified = true;
                await _context.SaveChangesAsync();
            }
            
            return new CommandResult(true);
        }

        private bool HasChangesAndApply(MentorsProfile profile, EditMentorProfileCommand command)
        {
            bool hasChanges = false;
            
            if (profile.Surname != command.Surname)
            {
                profile.Surname = command.Surname;
                hasChanges = true;
            }
            
            if(profile.MiddleName != command.MiddleName)
            {
                profile.MiddleName = command.MiddleName;
                hasChanges = true;
            }

            if(profile.AboutMe != command.AboutMe)
            {
                profile.AboutMe = command.AboutMe;
                hasChanges = true;
            }

            if(profile.TownId != command.TownId)
            {
                profile.TownId = command.TownId;
                hasChanges = true;
            }

            if(profile.StudyingPlace != command.StudyingPlace)
            {
                profile.StudyingPlace = command.StudyingPlace;
                hasChanges = true;
            }

            if(profile.Education != command.Education)
            {
                profile.Education = command.Education;
                hasChanges = true;
            }

            if (command.ProfileImageBytes?.Length > 0)
            {
                if (profile.ProfileImageId == null)
                    profile.ProfileImage = new MentorsProfileImage();
                
                byte[] imageData = command.ProfileImageBytes;
                imageData = ImageProcessor.MakeImageSquare(imageData);
                imageData = ImageProcessor.ReduceImage(imageData, 200);
                
                string imageBase64String = Convert.ToBase64String(imageData);
                if (profile.ProfileImage.Image != imageBase64String)
                {
                    profile.ProfileImage.Image = imageBase64String;
                    hasChanges = true;
                }
            }

            return hasChanges;
        }
    }
}