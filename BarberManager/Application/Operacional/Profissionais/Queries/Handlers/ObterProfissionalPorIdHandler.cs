using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Profissionais;
using Application.Operacional.Profissionais.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Profissionais.Queries.Handlers
{
    public class ObterProfissionalPorIdHandler : IQueryHandler<ObterProfissionalPorIdQuery, ProfissionalDetalheDto>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IRepositorioProfissionais _repositorio;

        public ObterProfissionalPorIdHandler(
        ITenantContext tenantContext,
        IRepositorioProfissionais repositorio)
        {
            _tenantContext = tenantContext;
            _repositorio = repositorio;
        }

        public async Task<ProfissionalDetalheDto> Handle(ObterProfissionalPorIdQuery query, CancellationToken cancellationToken)
        {
            if(!_tenantContext.IsAvailable || _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException("Não foi possivel identificar a empresa do usuario.");
            }

            if (query.ProfissionalId == Guid.Empty)
            {
                throw new CustomValidationException(
                    new[] { "O ID do profissional é inválido." });
            }

            var empresaId = _tenantContext.EmpresaId;

            var profissional = await _repositorio.ObterPorIdAsync(empresaId, query.ProfissionalId, cancellationToken);

            if(profissional is null)
            {
                throw new CustomValidationException(new[] { "Profissional não encontrado." });
            }

            var horarios = await _repositorio.ObterHorariosPorProfissionalAsync(
                empresaId, profissional.Id, cancellationToken);

            var horariosDto = horarios.Select(
                h => new HorarioProfissionalDto(
                    h.Id, h.DiaSemana, h.HoraInicio, h.HoraFim, h.Ativo)).ToList();

            var servicosDto = profissional.Servicos
                .Select(ps => new ProfissionalServicoDto(
                    ps.ServicoId, ps.Servico.Nome, ps.Servico.Valor,
                    ps.ValorPersonalizado,
                    ps.Servico.DuracaoMinutos, ps.DuracaoPersonalizada,
                    ps.Ativo.ToString()
                    )).ToArray();

            return new ProfissionalDetalheDto(
                profissional.Id,
            profissional.Nome,
            profissional.Sobrenome,
            profissional.DataNascimento,
            profissional.Sexo.ToString(),
            profissional.Cpf,
            profissional.Rg,
            profissional.Celular,
            profissional.Telefone,
            profissional.Email,
            profissional.Matricula,
            profissional.Cargo,
            profissional.DataAdmissao,
            profissional.DataDemissao,
            profissional.TipoComissao.ToString(),
            profissional.ValorTipoPagamento,
            profissional.IntervaloAtendimento,
            profissional.PermiteEncaixe,
            profissional.AgendaOnline,
            profissional.Biografia,
            profissional.Foto,
            profissional.Status.ToString(),
            profissional.ExibirAgendamentoOnline,
            profissional.RecebePagamentoDireto,
            profissional.OrdemAgenda,
            profissional.AvaliacaoMedia,
            profissional.TotalAvaliacoes,
            profissional.TotalComissaoMes,
            profissional.Instagram,
            profissional.Facebook,
            profissional.Especialidade,
            profissional.Destaque,
            horariosDto,
            servicosDto
                );
        }
    }
}
