using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Profissionais.DTOs
{
   

    public sealed record ProfissionalDetalheDto(
        Guid Id,
        string Nome,
        string? Sobrenome,
        DateTime? DataNascimento,
        string Sexo,
        string Cpf,
        string? Rg,
        string Celular,
        string? Telefone,
        string Email,
        string Matricula,
        string Cargo,
        DateTime DataAdmissao,
        DateTime? DataDemissao,
        string TipoComissao,
        decimal ValorTipoPagamento,
        int IntervaloAtendimento,
        bool PermiteEncaixe,
        bool AgendaOnline,
        string? Biografia,
        string? Foto,
        string Status,
        bool ExibirAgendamentoOnline,
        bool RecebePagamentoDireto,
        int OrdemAgenda,
        decimal AvaliacaoMedia,
        int TotalAvaliacoes,
        decimal TotalComissaoMes,
        string? Instagram,
        string? Facebook,
        string? Especialidade,
        bool Destaque,
        IReadOnlyCollection<HorarioProfissionalDto> Horarios,
        IReadOnlyCollection<ProfissionalServicoDto> Servicos
    );

    public sealed record HorarioProfissionalDto(
        Guid Id,
        DayOfWeek DiaSemana,
        TimeOnly HoraInicio,
        TimeOnly HoraFim,
        bool Ativo
    );

    public sealed record ProfissionalServicoDto(
        Guid ServicoId,
        string NomeServico,
        decimal ValorPadrao,
        decimal? ValorPersonalizado,
        int DuracaoPadraoMinutos,
        int? DuracaoPersonalizadaMinutos,
        string Status
    );
}
