using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Saas
{
    public class Empresa : EntityBase
    {

        public string? RazaoSocial { get; set; } = null!;
        public string NomeFantasia { get; set; } = null!;
        public string? Cnpj { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public StatusGeral Ativa { get; set; }

        public string? LogoUrl { get; set; }

        public DateTime? DataBloqueio { get; set; }
        public string? MotivoBloqueio { get; set; }


        //Navegação 
        public ICollection<Assinatura> Assinaturas { get; set; }
                                                = new List<Assinatura>();

        public string? ReferenciaClienteAsaas { get; set; }

        public Guid ResponsavelId { get; set; }
        public Responsavel Responsavel { get; set; } = null!;


        public ICollection<EmpresaModulo> EmpresaModulos { get; set; } = [];

    }
}
