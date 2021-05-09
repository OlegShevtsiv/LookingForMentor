using System.Collections.Generic;
using System.Threading.Tasks;
using Lfm.Domain.ReadModels.ReviewModels.Town;

namespace LFM.Domain.Read.Providers
{
    public interface IMasterDataProvider
    {
        Task<ICollection<TownPreviewModel>> GetAllTowns();
    }
}