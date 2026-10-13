using Domain.Entities.Common.Enuns;
using Domain.Entities.Saas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Planos.DTOs
{
    public class GetPlanoDto
    {
        public Guid Id { get; set; }

        public string Nome { get; set; } = null!;

        public decimal ValorMensal { get; set; }

        public decimal ValorAnual { get; set; }

        public int LimiteProfissionais { get; set; }

        public int LimiteEmpresa { get; set; }

        public string Ativo { get; set; }

    }
}
