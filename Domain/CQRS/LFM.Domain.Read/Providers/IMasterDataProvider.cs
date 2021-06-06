using System.Collections.Generic;
using System.Threading.Tasks;
using Lfm.Domain.ReadModels.ReviewModels.Subject;
using Lfm.Domain.ReadModels.ReviewModels.Town;

namespace LFM.Domain.Read.Providers
{
    public interface IMasterDataProvider
    {
        Task<ICollection<TownPreviewModel>> GetTownsList();

        Task<ICollection<SubjectListItem>> GetSubjectsList();
    }
}