using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LFM.DataAccess.DB.Core.Entities;
using LFM.DataAccess.DB.Core.Repository;
using Lfm.Domain.ReadModels.ReviewModels.StudentProfile;
using Microsoft.EntityFrameworkCore;

namespace LFM.Domain.Read.Providers.Implementations
{
    internal class StudentProfileProvider : IStudentProfileProvider
    {
        private readonly IRepository<OrderRequest> _orderReqRepo;
        private readonly IMapper _mapper;

        public StudentProfileProvider(
            IRepository<OrderRequest> orderReqRepo,
            IMapper mapper)
        {
            _orderReqRepo = orderReqRepo;
            _mapper = mapper;
        }

        public async Task<ICollection<FindMentorRequestReviewModel>> GetFindMentorRequests(int studentId)
        {
            var data = await _orderReqRepo.GetQueryable()
                .Where(m => m.StudentId == studentId)
                .ProjectTo<FindMentorRequestReviewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            
            return data;
        }
    }
}