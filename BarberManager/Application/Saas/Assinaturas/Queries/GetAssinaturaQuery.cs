using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Assinaturas.Queries
{
    public class GetAssinaturaQuery
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string? SearchTerm { get; set; }
        public string? SortBy { get; set; }
        public string? Status { get; set; }
        public string? Route { get; set; }
    }
}
