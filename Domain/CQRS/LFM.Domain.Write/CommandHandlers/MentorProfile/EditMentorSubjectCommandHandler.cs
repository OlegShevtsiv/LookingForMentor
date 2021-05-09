using System.Linq;
using System.Threading.Tasks;
using LFM.Core.Common.Exceptions;
using LFM.DataAccess.DB.Core.Context;
using LFM.DataAccess.DB.Core.Entities.MentorEntities;
using LFM.DataAccess.DB.Core.Entities.SubjectEntities;
using LFM.Domain.Write.Commands.MentorProfile;
using LFM.Domain.Write.Declarations;
using LFM.Domain.Write.Models;
using Microsoft.EntityFrameworkCore;

namespace LFM.Domain.Write.CommandHandlers.MentorProfile
{
    internal class EditMentorSubjectCommandHandler : ICommandHandler<EditMentorSubjectCommand, CommandResult>
    {
        private readonly LfmDbContext _context;

        public EditMentorSubjectCommandHandler(LfmDbContext context)
        {
            _context = context;
        }

        public async Task<CommandResult> ExecuteAsync(EditMentorSubjectCommand command)
        {
            if (command.TagIds?.Any() != true)
                throw new LfmException("No tags selected");

            var mentorSubject = await _context.MentorsSubjectsInfo
                .Include(s => s.Tags)
                .Where(m => m.MentorId == command.MentorId && m.SubjectId == command.SubjectId)
                .FirstOrDefaultAsync();
            
            if (mentorSubject == null)
                throw new LfmException(Messages.DataNotFound);
            
            if (!HasChangesAndApply(mentorSubject, command))
            {
                return new CommandResult(true);
            }
            
            Subject subject = await _context.Subjects
                .Include(s => s.Tags)
                .FirstOrDefaultAsync(s => s.Id == command.SubjectId);

            if (subject == null)
                throw new LfmException(Messages.DataNotFound);

            if (command.TagIds.Exists(t => subject.Tags.All(tg => tg.Id != t)))
                throw new LfmException("Invalid subject tags.");

            await _context.SaveChangesAsync();

            return new CommandResult(true);
        }

        private bool HasChangesAndApply(MentorsSubjectInfo subjectInfo, EditMentorSubjectCommand command)
        {
            bool hasChanges = false;

            if (subjectInfo.CostPerHour != command.CostPerHour)
            {
                subjectInfo.CostPerHour = command.CostPerHour;
                hasChanges = true;
            }
            
            if (subjectInfo.Description != command.Description)
            {
                subjectInfo.Description = command.Description;
                hasChanges = true;
            }

            var currentTags = subjectInfo.Tags.Select(t => t.TagId).ToList();
            if (currentTags.Count != command.TagIds.Count || !currentTags.All(command.TagIds.Contains))
            {
                subjectInfo.Tags = command.TagIds.Select(t => new MentorsSubjectTag
                {
                    TagId = t,
                    MentorsSubjectInfoId = subjectInfo.Id
                }).ToList();
                hasChanges = true;
            }

            return hasChanges;
        }
    }
}