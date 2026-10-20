using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Planos.DTOs
{
    public class PlanoPublicoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal ValorMensal { get; set; }
        public decimal ValorAnual { get; set; }
        public int LimiteProfissionais { get; set; }
        public int LimiteEmpresas { get; set; }

        public List<ModuloPlanoDto> Modulos { get; set; }
    }

    public class ModuloPlanoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }

    }
}
