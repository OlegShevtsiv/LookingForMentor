using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LFM.Core.Common.Exceptions;
using LFM.DataAccess.DB.Core.Entities.SubjectEntities;
using LFM.DataAccess.DB.Core.Repository;
using LFM.Domain.Read.Caching;
using Lfm.Domain.ReadModels.ReviewModels.Subject;
using Microsoft.EntityFrameworkCore;

namespace LFM.Domain.Read.Providers.Implementations
{
    internal class SubjectsProvider : ISubjectsProvider
    {
        private readonly IRepository<Subject> _subjectsRepo;
        private readonly IMapper _mapper;
        private readonly SubjectCachingService _subjectsCachingService;

        public SubjectsProvider(
            IRepository<Subject> subjectsRepo, 
            IMapper mapper, 
            SubjectCachingService cachingService)
        {
            _subjectsRepo = subjectsRepo;
            _mapper = mapper;
            _subjectsCachingService = cachingService;
        }

        public async Task<ICollection<SubjectReviewModel>> GetAllSubjects()
        {
            ICollection<SubjectReviewModel> subjects;
            
            if (!await _subjectsCachingService.TryGetAllSubjects(out subjects))
            {
                subjects = await _subjectsRepo.GetQueryable()
                    .ProjectTo<SubjectReviewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                if (!subjects.Any())
                    throw new LfmException(Messages.DataNotFound);

                await _subjectsCachingService.TryCacheAllSubjects(subjects);
            }
            
            return subjects;
        }
        
        public async Task<SubjectReviewModel> GetSubject(int subjectId)
        {
            SubjectReviewModel subject;

            if (!await _subjectsCachingService.TryGetById(subjectId, out subject))
            {
                subject = await _subjectsRepo.GetQueryable()
                    .Where(s => s.Id == subjectId)
                    .ProjectTo<SubjectReviewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                if (subject == null)
                    throw new LfmException(Messages.DataNotFound);
            }
            
            return subject;
        }
    }
}