using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using LFM.Core.Common.Data;
using LFM.Core.Common.Exceptions;
using LFM.DataAccess.DB.Core.Entities;
using LFM.DataAccess.DB.Core.Resources;

namespace LFM.DataAccess.DB.Core.MasterDataProviders
{
    internal class TownsResourceProvider
    {
        public Task<ICollection<Town>> GetAllTowns()
        {
            ResourceSet townsResource =
                UkrainianTowns.ResourceManager.GetResourceSet(Thread.CurrentThread.CurrentCulture, true, true);
            
            if (townsResource == null)
            {
                throw new LfmException(Messages.DataNotFound);
            }

            ICollection<Town> towns = townsResource.Cast<DictionaryEntry>()
                .Select(item => new Town
                {
                    Id = int.Parse(item.Key.ToString()), 
                    Name = item.Value?.ToString()
                })
                .OrderBy(t => t.Id).ToList();

            return Task.FromResult(towns);
        }
    }
}