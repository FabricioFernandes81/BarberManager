using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Pagination
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string? SearchTerm { get; set; }

        public string? SortBy { get; set; }

        public string? Status { get; set; }
        public PaginationFilter()
        {

            this.PageNumber = 1;
            this.PageSize = 10;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {

            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 50 ? 50 : (pageSize < 1 ? 10 : pageSize);
            //this.SearchTerm = SearchTerm ?? string.Empty;
            //
        }
    }
}
