using System.Collections.Generic;
using System.Threading.Tasks;
using LFM.Domain.Read.EntityProvideServices;
using Lfm.Domain.ReadModels.ReviewModels.Subject;
using Lfm.Domain.ReadModels.ReviewModels.Town;

namespace LFM.Domain.Read.Providers.Implementations
{
    internal class MasterDataProvider : IMasterDataProvider
    {
        private readonly TownsProvideService _townsProvideService;
        private readonly SubjectProvideService _subjectProvideService;

        public MasterDataProvider(
            TownsProvideService townsProvideService,
            SubjectProvideService subjectProvideService)
        {
            _townsProvideService = townsProvideService;
            _subjectProvideService = subjectProvideService;
        }

        public async Task<ICollection<TownPreviewModel>> GetTownsList()
        {
            return await _townsProvideService.GetTowns();
        }
        
        public async Task<ICollection<SubjectListItem>> GetSubjectsList()
        {
            return await _subjectProvideService.GetSubjects<SubjectListItem>();
        }
    }
}