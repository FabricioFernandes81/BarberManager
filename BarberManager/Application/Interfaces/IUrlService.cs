using Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUrlService
    {
        Uri GetPageUri(PaginationFilter filter, string route);

    }
}
