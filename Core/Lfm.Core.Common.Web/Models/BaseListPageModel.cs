using System.Collections.Generic;

namespace Lfm.Core.Common.Web.Models
{
    public abstract class AbstractListPageModel<TReviewModel, TSearchModel> 
        : BaseListPageModel<TReviewModel>
        where TReviewModel : class, new() 
        where TSearchModel : class, new()
    {
        public TSearchModel SearchModel { get; }

        public AbstractListPageModel(PageList<TReviewModel> items, TSearchModel searchModel, int pageSize) 
            : base(items, pageSize)
        {
            SearchModel = searchModel;
        }

        public abstract Dictionary<string, string> GetAllRouteData();
    }
    
    public class BaseListPageModel<TReviewModel>
        where TReviewModel : class, new()
    {
        public PaginationModel Pagination { get; }

        public PageList<TReviewModel> PageList { get; }
        
        public BaseListPageModel(PageList<TReviewModel> items, int pageSize)
        {
            Pagination = new PaginationModel(items.TotalCount, items.PageNumber, pageSize);
            PageList = items;
        }
    }
}