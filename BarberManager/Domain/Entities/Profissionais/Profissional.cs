using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Profissionais
{
    public class Profissional : EntityBase
    {

        public Guid EmpresaId { get; set; }

        public string Nome { get; set; }

        public string? Sobrenome { get; set; }
        public DateTime? DataNascimento { get; set; }

        public EnumSexo Sexo { get; set; }

        public string Cpf { get; set; } = string.Empty;

        public string? Rg { get; set; }

        public string Celular { get; set; } = string.Empty;

        public string? Telefone { get; set; }

        public string Email { get; set; } = string.Empty;



        public string Matricula { get; set; } = string.Empty;

        public string Cargo { get; set; } = string.Empty;

        public DateTime DataAdmissao { get; set; }

        public DateTime? DataDemissao { get; set; }


         public TipoPagamento TipoComissao { get; set; }

          public decimal ValorTipoPagamento { get; set; }

        // Configuração da Agenda

        public int IntervaloAtendimento { get; set; }

        public bool PermiteEncaixe { get; set; }

        public bool AgendaOnline { get; set; }

        public string? Biografia { get; set; }

        public string? Foto { get; set; }

        public int? ToleranciaEncerramentoMinutos { get; set; }
        // Situação

        public StatusProfissional Status { get; set; }


        public bool ExibirAgendamentoOnline { get; set; }

        public bool RecebePagamentoDireto { get; set; }

        public int OrdemAgenda { get; set; }

        public decimal AvaliacaoMedia { get; set; }

        public int TotalAvaliacoes { get; set; }

        public decimal TotalComissaoMes { get; set; }

        public string? Instagram { get; set; }

        public string? Facebook { get; set; }

        public string? Especialidade { get; set; }

        public bool Destaque { get; set; }

        public Guid? UsuarioId { get; set; }


        public ICollection<ProfissionalServicos> Servicos { get; set; }
                           = new List<ProfissionalServicos>();
    }
}
