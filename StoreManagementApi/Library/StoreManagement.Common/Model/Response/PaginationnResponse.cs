using System.Collections.Generic;

namespace StoreManagement.Common.Model.Response
{
    public class PaginationResponse<T>
    {
        public PaginationResponse(List<T> records, int totalRecords, int pageNumber, int pageSize)
        {
            this.records = records;
            this.totalRecords = totalRecords;
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
        }

        public int pageNumber { get; set; }

        public int pageSize { get; set; }

        public int totalRecords { get; set; }

        public List<T> records { get; set; }

    }
}