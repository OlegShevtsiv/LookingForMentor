using System.Linq;
using System.Threading.Tasks;
using LFM.Core.Common.Data;
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
    internal class AddMentorSubjectCommandHandler : ICommandHandler<AddMentorSubjectCommand, CommandResult>
    {
        private readonly LfmDbContext _context;

        public AddMentorSubjectCommandHandler(LfmDbContext context)
        {
            _context = context;
        }

        public async Task<CommandResult> ExecuteAsync(AddMentorSubjectCommand command)
        {
            if (command.TagIds?.Any() != true)
                throw new LfmException("No tags selected");

            var mentorSubjects = await _context.MentorsProfiles
            .Where(m => m.MentorId == command.MentorId)
            .Select(m => m.SubjectsInfo)
            .FirstOrDefaultAsync();

            if (mentorSubjects == null)
                throw new LfmException(Messages.SystemError);

            Subject subject = await _context.Subjects
                .Include(s => s.Tags)
                .FirstOrDefaultAsync(s => s.Id == command.SubjectId);

            if (subject == null)
                throw new LfmException(Messages.DataNotFound);
            
            if (mentorSubjects.Exists(p => p.SubjectId == command.SubjectId))
                throw new LfmException($"Subject '{subject.Name}' already added.");
            
            if (command.TagIds.Exists(t => subject.Tags.All(tg => tg.Id != t)))
                throw new LfmException("Invalid subject tags.");

            var selectedTags = command.TagIds
                .Select(t => new MentorsSubjectTag {TagId = t})
                .ToList();
            
            await _context.MentorsSubjectsInfo.AddAsync(new MentorsSubjectInfo
            {
                MentorId = command.MentorId,
                CostPerHour = command.CostPerHour,
                Description = command.Description,
                SubjectId = command.SubjectId,
                Tags = selectedTags
            });

            await _context.SaveChangesAsync();

            return new CommandResult(true);
        }
    }
}