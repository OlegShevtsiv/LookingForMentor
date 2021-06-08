using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using LFM.Core.Common.Data;
using LFM.Core.Common.Exceptions;
using Lfm.Core.Common.Web.Data;

namespace Lfm.Core.Common.Web.Models
{
    [Serializable]
    public class PaginationModel
    {
        [JsonPropertyName("pn")]
        public int PageNumber { get; private set; }

        [JsonPropertyName("tps")]
        public int TotalPages { get; private set; }
        
        private uint _pagesDimension;
        private List<int> _numbersAllNextPages = new List<int>();
        private List<int> _numbersAllPreviousPages = new List<int>();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordsCount"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="dim">Should be multiple 2</param>
        /// <exception cref="LfmException"></exception>
        public PaginationModel(
            int recordsCount, 
            int pageNumber, 
            int pageSize = Constants.DefaultPageSize, 
            uint dim = Constants.DefaultPaginationDimension)
        {
            if (recordsCount < 1)
                return;

            if (pageNumber < 1 || pageSize < 1)
            {
                throw new LfmException(Messages.InvalidRequest);
            }

            this.TotalPages = (int)Math.Ceiling(recordsCount / (double)pageSize);
            
            if (pageNumber > TotalPages)
            {
                throw new LfmException(Messages.InvalidRequest);
            }

            this.PageNumber = pageNumber;
            this.SetDimension(dim);
        }

        public void SetDimension(uint amount)
        {
            if (amount < 2)
            {
                _pagesDimension = 2;
            }
            else if (amount % 2 == 1)
            {
                _pagesDimension = amount - 1;
            }
            else _pagesDimension = amount;

            this.InitNumbersPrevBeforePages();
        }

        public List<int> GetNumbersAllPreviousPages() => _numbersAllPreviousPages;
        public List<int> GetNumbersAllNextPages() => _numbersAllNextPages;

        private void InitNumbersPrevBeforePages()
        {
            int i = PageNumber;
            int j = PageNumber;

            while (i - j < _pagesDimension)
            {
                if (j == 1 && i == TotalPages)
                {
                    break;
                }
                
                if (j > 1)
                {
                    j--;
                    _numbersAllPreviousPages.Insert(0, j);
                }

                if (i < TotalPages)
                {
                    i++;
                    _numbersAllNextPages.Add(i);
                }
            }
        }
    }
}