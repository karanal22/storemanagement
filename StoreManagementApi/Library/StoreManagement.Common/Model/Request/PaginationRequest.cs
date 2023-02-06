using System.Collections.Generic;

namespace StoreManagement.Common.Model.Request
{
    public class PaginationRequest
    {
        public PaginationRequest()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

		public List<Dictionary<string, string>> SortBy { get; set; }

		public int GetPageIndex()
        {
            if (PageNumber < 1)
            {
                return 0;
            }

            return PageNumber - 1;
        }

        public int GetSkip()
        {
            return GetPageIndex() * PageSize;
        }

        public int GetTake()
        {
            return PageSize;
        }
    }
}