using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LFM.Core.Common.Exceptions;
using LFM.DataAccess.DB.Core.Entities.SubjectEntities;
using LFM.DataAccess.DB.Core.Repository;
using LFM.Domain.Read.Caching;
using LFM.Domain.Read.EntityProvideServices;
using Lfm.Domain.ReadModels.ReviewModels.Subject;
using Microsoft.EntityFrameworkCore;

namespace LFM.Domain.Read.Providers.Implementations
{
    internal class SubjectsProvider : ISubjectsProvider
    {
        private readonly IRepository<Subject> _subjectsRepo;
        private readonly IMapper _mapper;
        private readonly SubjectCachingService _subjectsCachingService;
        private readonly SubjectProvideService _subjectProvideService;

        public SubjectsProvider(
            IRepository<Subject> subjectsRepo, 
            IMapper mapper, 
            SubjectCachingService cachingService)
        {
            _subjectsRepo = subjectsRepo;
            _mapper = mapper;
            _subjectsCachingService = cachingService;
            _subjectProvideService = new SubjectProvideService(_subjectsRepo, _mapper, _subjectsCachingService);
        }

        public async Task<IEnumerable<SubjectReviewModel>> GetAllSubjects()
        {
            return (await _subjectProvideService.GetSubjects<SubjectReviewModel>()).ToList();
        }

        public async Task<IEnumerable<SubjectListItem>> GetSubjectsList()
        {
            var subjectsList = await _subjectProvideService.GetSubjects<SubjectListItem>();

            return subjectsList;
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

        public async Task<bool> IsExists(int subjectId)
        {
            if (!await _subjectsCachingService.TryGetById(subjectId, out _))
            {
                return await _subjectsRepo.GetQueryable()
                    .AnyAsync(s => s.Id == subjectId);
            }

            return true;
        }
    }
}