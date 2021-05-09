using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LFM.DataAccess.DB.Core.MasterDataProviders;
using Lfm.Domain.ReadModels.ReviewModels.Town;

namespace LFM.Domain.Read.Providers.Implementations
{
    internal class MasterDataProvider : IMasterDataProvider
    {
        private readonly TownsResourceProvider _townsResourceProvider;

        public MasterDataProvider(TownsResourceProvider townsResourceProvider)
        {
            _townsResourceProvider = townsResourceProvider;
        }

        public async Task<ICollection<TownPreviewModel>> GetAllTowns()
        {
            return (await _townsResourceProvider.GetAllTowns()).Select(t => new TownPreviewModel
            {
                Id = t.Id,
                Name = t.Name
            }).ToList();
        }
    }
}