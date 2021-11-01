using System.Collections.Generic;
using Lfm.Core.Common.Web.Models;
using Lfm.Domain.ReadModels.Common;
using Lfm.Domain.ReadModels.ReviewModels.Mentor;
using Lfm.Domain.ReadModels.SearchModels;

namespace Lfm.Web.Mvc.Models
{
    public class MentorPageModel
    {
        public PaginationModel Pagination { get; }

        public PageList<MentorPreviewModel> PageList { get; }

        public MentorsSearchModel SearchModel { get; }

        public MentorPageModel(PageList<MentorPreviewModel> items, MentorsSearchModel searchModel, int pageSize)
        {
            Pagination = new PaginationModel(items.TotalCount, items.PageNumber, pageSize);
            PageList = items;
            SearchModel = searchModel;
        }

        public Dictionary<string, string> GetAllRouteData()
        {
            return new Dictionary<string, string>
            {
                { nameof(MentorsSearchModel.SubjectId), SearchModel.SubjectId.ToString() },
                { nameof(MentorsSearchModel.StudyingPlace), SearchModel.StudyingPlace.ToString() },
                { nameof(MentorsSearchModel.TownId), SearchModel.TownId.ToString() }
            };
        }
    }
}