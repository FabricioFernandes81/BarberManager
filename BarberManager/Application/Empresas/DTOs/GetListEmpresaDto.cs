using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Empresas.DTOs
{
    public class GetListEmpresaDto
    {
        public string Id { get; set; }
        public string NameFantasia { get; set; }
        public string CNPJ { get; set; }
        public string Plano { get; set; }
        public string Status { get; set; }

    }
}
