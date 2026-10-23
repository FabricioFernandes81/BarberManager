using Application.Common.Transactions;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Profissionais.Commands
{
    public sealed record HorarioProfissionalAtualizacaoInput(
    Guid? Id,
    DayOfWeek DiaSemana,
    TimeOnly HoraInicio,
    TimeOnly HoraFim);

    public sealed record AtualizarProfissionalCommand(
        Guid ProfissionalId,
        string Nome,
        string? Sobrenome,
        DateTime? DataNascimento,
        EnumSexo Sexo,
        string Cpf,
        string? Rg,
        string Celular,
        string? Telefone,
        string Email,
        string Matricula,
        string Cargo,
        DateTime DataAdmissao,
        DateTime? DataDemissao,
        TipoPagamento TipoComissao,
        decimal ValorTipoPagamento,
        int IntervaloAtendimento,
        bool PermiteEncaixe,
        bool AgendaOnline,
        string? Biografia,
        string? Foto,
        StatusProfissional Status,
        bool ExibirAgendamentoOnline,
        bool RecebePagamentoDireto,
        int OrdemAgenda,
        string? Instagram,
        string? Facebook,
        string? Especialidade,
        bool Destaque,
        IReadOnlyCollection<HorarioProfissionalAtualizacaoInput> Horarios
    ) : ITransactionalCommand
    {
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}
