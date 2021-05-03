using System.Collections.Generic;
using System.Linq;

namespace LFM.Core.Common.Extensions
{
    public static class CollectionsExtensions
    {
        public static string ToCommaSeparatedString(this IEnumerable<string> collection)
        {
            return collection?.Aggregate(string.Empty, (curr, next) => $"{curr},{next}").Trim(',');
        }
    }
}