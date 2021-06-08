using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LFM.Core.Common.Data;
using LFM.Core.Common.Exceptions;
using LFM.DataAccess.DB.Core.Entities;
using LFM.DataAccess.DB.Core.Entities.MentorEntities;
using LFM.DataAccess.DB.Core.Repository;
using LFM.Domain.Read.EntityProvideServices;
using Lfm.Domain.ReadModels.ReviewModels.MentorProfile;
using Lfm.Domain.ReadModels.ReviewModels.Subject;
using Microsoft.EntityFrameworkCore;

namespace LFM.Domain.Read.Providers.Implementations
{
    internal class MentorProfileProvider : IMentorProfileProvider
    {
        private readonly IRepository<MentorsProfile> _mentorsProfileRepo;
        private readonly IRepository<MentorsSubjectInfo> _mentorsSubjectInfo;
        private readonly IRepository<OrderRequest> _personalOrderRepo;
        private readonly IRepository<ApprovedOrder> _approvedOrderRepo;
        private readonly IMapper _mapper;
        private readonly SubjectProvideService _subjectProvideService;

        public MentorProfileProvider(
            IRepository<MentorsProfile> mentorsProfileRepo,
            IRepository<MentorsSubjectInfo> mentorsSubjectInfo,
            IRepository<OrderRequest> personalOrderRepo, 
            IRepository<ApprovedOrder> approvedOrderRepo,
            IMapper mapper, 
            SubjectProvideService subjectProvideService)
        {
            _mentorsProfileRepo = mentorsProfileRepo;
            _mentorsSubjectInfo = mentorsSubjectInfo;
            _personalOrderRepo = personalOrderRepo;
            _approvedOrderRepo = approvedOrderRepo;
            _mapper = mapper;
            _subjectProvideService = subjectProvideService;
        }

        public async Task<T> GetGeneralInfo<T>(int mentorId) where T : class
        {
            T mentorsProfile = await _mentorsProfileRepo.GetQueryable()
                .Where(m => m.MentorId == mentorId) 
                .ProjectTo<T>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
            
            if (mentorsProfile == null)
                throw new LfmException(Messages.DataNotFound);

            return mentorsProfile;
        }

        public async Task<ICollection<MentorSubjectReviewModel>> GetSubjectsInfo(int mentorId)
        {
            var subjectsInfo = await _mentorsSubjectInfo.GetQueryable()
                .Where(m => m.MentorId == mentorId)
                .ProjectTo<MentorSubjectReviewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            
            return subjectsInfo;
        }

        public async Task<ICollection<SubjectListItem>> GetAvailableSubjects(int mentorId)
        {
            var subjects = (await _subjectProvideService.GetSubjects<SubjectListItem>()).ToList();
            
            var existingSubjects = await _mentorsSubjectInfo.GetQueryable()
                .Where(m => m.MentorId == mentorId)
                .Select(m => m.SubjectId)
                .ToListAsync();

            return subjects.Where(s => !existingSubjects.Contains(s.Id)).ToList();
        }

        public async Task<MentorSubjectReviewModel> GetSubject(int mentorId, int subjectId)
        {
            var subject = await _mentorsSubjectInfo.GetQueryable()
                .Where(m => m.MentorId == mentorId && m.SubjectId == subjectId)
                .ProjectTo<MentorSubjectReviewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return subject;
        }

        public async Task<bool> CanAddSubject(int mentorId, int subjectId)
        {
            var query = _mentorsProfileRepo.GetQueryable()
                .Where(m => m.MentorId == mentorId);

            if ((await query.CountAsync()) != 1)
                throw new LfmException(Messages.DataNotFound, "User");

            return !await query.AnyAsync(m => m.SubjectsInfo.Any(s => s.SubjectId == subjectId));
        }

        public async Task<byte[]> GetAvatar(int mentorId)
        {
            var avatar = await _mentorsProfileRepo.GetQueryable()
                .Where(m => m.MentorId == mentorId)
                .Select(m => m.ProfileImage)
                .SingleOrDefaultAsync();

            if (!string.IsNullOrWhiteSpace(avatar?.Image))
            {
                return Convert.FromBase64String(avatar.Image);
            }

            return null;
        }
        
        public async Task<ICollection<T>> GetPersonalOrdersRequests<T>(int mentorId) where T : MentorsOrderMinReviewModel
        {
            var data = await _personalOrderRepo.GetQueryable()
                .Where(m => m.MentorId == mentorId)
                .ProjectTo<T>(_mapper.ConfigurationProvider)
                .ToListAsync();
            
            return data;
        }
        
        public async Task<MentorPersonalOrderDetailedReviewModel> GetPersonalOrderRequestDetails(int mentorId, int orderId)
        {
            var order = await _personalOrderRepo.GetQueryable()
                .Where(m => m.MentorId == mentorId && m.Id == orderId)
                .ProjectTo<MentorPersonalOrderDetailedReviewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (order == null)
                throw new LfmException(Messages.DataNotFound);

            return order;
        }

        public async Task<ICollection<T>> GetMentorsOrders<T>(int mentorId) where T : MentorsOrderMinReviewModel
        {
            var data = await _approvedOrderRepo.GetQueryable()
                .Where(m => m.MentorId == mentorId)
                .ProjectTo<T>(_mapper.ConfigurationProvider)
                .ToListAsync();
            
            return data;
        }
        
        public async Task<MentorsOrderDetailedReviewModel> GetMentorsOrderDetails(int mentorId, int orderId)
        {
            var order = await _approvedOrderRepo.GetQueryable()
                .Where(m => m.MentorId == mentorId && m.Id == orderId)
                .ProjectTo<MentorsOrderDetailedReviewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (order == null)
                throw new LfmException(Messages.DataNotFound);

            return order;
        }
    }
}