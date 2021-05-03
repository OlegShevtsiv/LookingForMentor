using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LFM.Core.Common.Exceptions;
using LFM.DataAccess.DB.Core.Entities.MentorEntities;
using LFM.DataAccess.DB.Core.Repository;
using LFM.DataAccess.DB.Core.Types;
using Lfm.Domain.Common.Services.Role;
using Microsoft.EntityFrameworkCore;

namespace LFM.Domain.Read.Providers.Implementations
{
    internal class MentorProfileProvider : IMentorProfileProvider
    {
        private readonly IRepository<MentorsProfile> _mentorsProfileRepo;
        private readonly IMapper _mapper;
        private readonly ILfmRoleManager _roleManager;

        public MentorProfileProvider(
            IRepository<MentorsProfile> mentorsProfileRepo,
            IMapper mapper,
            ILfmRoleManager roleManager)
        {
            _mentorsProfileRepo = mentorsProfileRepo;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<T> GetGeneralInfo<T>(int mentorId) where T : class
        {
            if ((await _roleManager.RetrieveUserRole(mentorId)) != LfmIdentityRolesEnum.Mentor)
                throw new LfmException(Messages.DataNotFound);
            
            T mentorsProfile = await _mentorsProfileRepo.GetQueryable()
                .Where(m => m.MentorId == mentorId) 
                .ProjectTo<T>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            return mentorsProfile;
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
    }
}