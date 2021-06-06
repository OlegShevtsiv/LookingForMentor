using System.Collections;
using System.Collections.Generic;

namespace Lfm.Domain.ReadModels.Common
{
    public class PageModel<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _data;

        public int TotalCount { get; }

        public int PageNumber { get; }

        public PageModel(IEnumerable<T> data, int totalCount, int pageNumber = 1)
        {
            this._data = data;
            this.TotalCount = totalCount;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            foreach(T val in _data)
            {
                yield return val;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<T> GetData() => _data;
    }
}