using Application.Common.Transactions;
using Application.Operacional.Profissionais.DTOs;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Profissionais.Commands
{
    public class HorarioProfissionalInput
    {
        public DayOfWeek DiaSemana { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFim { get; set; }
    }

    public class CriarProfissionalCommand : ITransactionalCommand
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Matricula { get; set; }
        public string Cargo { get; set; }
        public DateTime DataAdmissao { get; set; }
        public List<HorarioProfissionalInput>? Horarios { get; set; } = null;
        public string? Sobrenome { get; set; } = null;
        public DateTime? DataNascimento { get; set; } = null;
        public EnumSexo Sexo { get; set; } = EnumSexo.NaoInformado;
        public string? Rg { get; set; } = null;
        public string? Telefone { get; set; } = null;
        public TipoPagamento TipoComissao { get; set; } = TipoPagamento.Nenhuma;
        public decimal ValorTipoPagamento { get; set; } = 0;
        public int IntervaloAtendimento { get; set; } = 0;
        public bool PermiteEncaixe { get; set; } = false;
        public bool AgendaOnline { get; set; } = false;
        public string? Biografia { get; set; } = null;
        public string? Foto { get; set; } = null;
        public StatusProfissional Status { get; set; } = StatusProfissional.Ativo;
        public bool ExibirAgendamentoOnline { get; set; } = false;
        public bool RecebePagamentoDireto { get; set; } = false;
        public int OrdemAgenda { get; set; } = 0;
        public string? Instagram { get; set; } = null;
        public string? Facebook { get; set; } = null;
        public string? Especialidade { get; set; } = null;
        public bool Destaque { get; set; } = false;

        public TransactionContext TransactionContext => TransactionContext.Operacional;
    }

    /* public sealed record HorarioProfissionalInput(
     DayOfWeek DiaSemana,
     TimeOnly HoraInicio,
     TimeOnly HoraFim);

     public sealed record CriarProfissionalCommand(
         string Nome,
         string Cpf,
         string Celular,
         string Email,
         string Matricula,
         string Cargo,
         DateTime DataAdmissao,
         List<HorarioProfissionalInput>? Horarios = null,
         string? Sobrenome = null,
         DateTime? DataNascimento = null,
         EnumSexo Sexo = EnumSexo.NaoInformado,
         string? Rg = null,
         string? Telefone = null,
         TipoPagamento TipoComissao = TipoPagamento.Nenhuma,
         decimal ValorTipoPagamento = 0,
         int IntervaloAtendimento = 0,
         bool PermiteEncaixe = false,
         bool AgendaOnline = false,
         string? Biografia = null,
         string? Foto = null,
         StatusProfissional Status = StatusProfissional.Ativo,
         bool ExibirAgendamentoOnline = false,
         bool RecebePagamentoDireto = false,
         int OrdemAgenda = 0,
         string? Instagram = null,
         string? Facebook = null,
         string? Especialidade = null,
         bool Destaque = false
     ) : ITransactionalCommand
     {
         public TransactionContext TransactionContext =>
             TransactionContext.Operacional;
     }*/
}
