using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lfm.Domain.ReadModels.ReviewModels.Town;

namespace LFM.Domain.Read.Caching
{
    internal class TownsCachingService
    {
        private readonly ConcurrentDictionary<int, (ICollection<TownPreviewModel> Towns, DateTime ExpirationDateTime)> _townsCacheStorage;

        public TownsCachingService()
        {
            _townsCacheStorage = new ConcurrentDictionary<int, (ICollection<TownPreviewModel>, DateTime)>();
        }

        public Task<bool> TryGetAllTowns(out ICollection<TownPreviewModel> towns)
        {
            towns = null;
            if (_townsCacheStorage.Count == 1)
            {
                if (DateTime.UtcNow >= _townsCacheStorage.First().Value.ExpirationDateTime)
                {
                    _townsCacheStorage.Clear();
                }
                else
                {
                    towns = _townsCacheStorage.First().Value.Towns;
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }

        public Task<bool> TryCacheAllTowns(ICollection<TownPreviewModel> towns)
        {
            _townsCacheStorage.Clear();
            
            var expirationDateTime = DateTime.UtcNow.AddHours(24);

            bool isAdded = _townsCacheStorage.TryAdd(0, (towns, expirationDateTime));
            
            return Task.FromResult(isAdded);
        }
    }
}