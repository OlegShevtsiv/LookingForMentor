using System.Collections.Generic;
using System.Threading.Tasks;
using Lfm.Core.Common.Web.Models;
using Lfm.Domain.Manager.Models.ReviewModels;
using Lfm.Domain.Manager.Models.SearchModel;

namespace Lfm.Domain.Manager.Services.DataProviders
{
    public interface IToDoProvider
    {
        Task<PageList<PendingToDoReviewModel>> SearchPendingToDos(SearchToDosModel searchModel, int pageNo,
            int? pageSize = null);

        Task<ICollection<OperationReviewModel>> GetPerformingOperations();

        Task<ICollection<ToDoUserReviewModel>> GetPerformingUsers();
    }
}