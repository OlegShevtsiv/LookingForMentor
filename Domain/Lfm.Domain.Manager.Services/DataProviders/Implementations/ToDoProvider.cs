using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LFM.Core.Common.Extensions;
using Lfm.Core.Common.Web.Extensions;
using Lfm.Core.Common.Web.Models;
using LFM.DataAccess.DB.Core.Entities.ToDoEntities;
using LFM.DataAccess.DB.Core.Repository;
using LFM.DataAccess.DB.Core.Types;
using Lfm.Domain.Manager.Models.ReviewModels;
using Lfm.Domain.Manager.Models.SearchModel;
using Microsoft.EntityFrameworkCore;

namespace Lfm.Domain.Manager.Services.DataProviders.Implementations
{
    public class ToDoProvider : IToDoProvider
    {
        private readonly IRepository<ToDoEntity> _toDoRepo;
        private readonly IMapper _mapper;

        public ToDoProvider(
            IRepository<ToDoEntity> toDoRepo, 
            IMapper mapper)
        {
            _toDoRepo = toDoRepo;
            _mapper = mapper;
        }

        public Task<PageList<PendingToDoReviewModel>> SearchPendingToDos(SearchToDosModel searchModel, int pageNo, int? pageSize = null)
        {
            var query = _toDoRepo.GetQueryable()
                .Where(t => t.StatusId == (int) ToDoStatusEnum.Pending)
                .AddConditionWhen(t => t.OperationCodeId == searchModel.OperationId, searchModel?.OperationId != null)
                .AddConditionWhen(t => t.CreatedByUserId == searchModel.UserId, searchModel?.UserId != null)
                .OrderBy(t => t.CreatedDateTime)
                .ProjectTo<PendingToDoReviewModel>(_mapper.ConfigurationProvider);
            
            return query.GetPageList(pageNo, pageSize);
        }

        public async Task<ICollection<OperationReviewModel>> GetPerformingOperations()
        {
            return await _toDoRepo.GetQueryable()
                .Where(t => t.StatusId == (int) ToDoStatusEnum.Pending)
                .Select(t => new OperationReviewModel
                {
                    Id = t.OperationCodeId,
                    Name = t.Operation.Description
                })
                .OrderBy(o => o.Name)
                .ToListAsync();
        }
        
        public async Task<ICollection<ToDoUserReviewModel>> GetPerformingUsers()
        {
            return await _toDoRepo.GetQueryable()
                .Where(t => t.StatusId == (int) ToDoStatusEnum.Pending)
                .Select(t => new ToDoUserReviewModel
                {
                    Id = t.CreatedByUserId,
                    Email = t.CreatedByUser.Email
                })
                .OrderBy(u => u.Email)
                .ToListAsync();
        }
    }
}