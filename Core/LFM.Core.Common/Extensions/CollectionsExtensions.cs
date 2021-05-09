using System;
using System.Collections.Generic;
using System.Linq;

namespace LFM.Core.Common.Extensions
{
    public static class CollectionsExtensions
    {
        public static string ToSeparatedString(this IEnumerable<string> collection, string separator)
        {
            var list = collection?.ToList() ?? new List<string>();
            
            if (list.Any() != true)
                return string.Empty;

            if (list.Count == 1)
                return list.First();
            
            return String.Join(separator, list);
        }
    }
}