using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Common.Tenancy;
using Application.Interfaces.Agendamentos;
using Application.Operacional.Agendamentos.DTOs;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Agendamentos.Commands.Handlers
{
    public sealed class ConcluirAtendimentoHandler
     : ICommandHandler<
         ConcluirAtendimentoCommand,
         ConcluirAtendimentoDto>
    {
        private readonly IRepositorioGerenciarAgendamento
            _repositorioAgendamento;

        private readonly ITenantContext _tenantContext;

        private readonly IUsuarioLogado _usuarioLogado;

        public ConcluirAtendimentoHandler(
            IRepositorioGerenciarAgendamento repositorioAgendamento,
            ITenantContext tenantContext,
            IUsuarioLogado usuarioLogado)
        {
            _repositorioAgendamento = repositorioAgendamento;
            _tenantContext = tenantContext;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<ConcluirAtendimentoDto> Handle(
            ConcluirAtendimentoCommand command,
            CancellationToken cancellationToken)
        {
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "Não foi possível identificar a empresa do usuário."
                    });
            }

            if (!_usuarioLogado.IsAuthenticated)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "O usuário precisa estar autenticado."
                    });
            }

            if (command.AgendamentoId == Guid.Empty)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "O identificador do agendamento é obrigatório."
                    });
            }

            var agendamento =
                await _repositorioAgendamento.ObterParaAtualizacaoAsync(
                    _tenantContext.EmpresaId,
                    command.AgendamentoId,
                    cancellationToken);

            if (agendamento is null)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "Agendamento não encontrado."
                    });
            }

            if (agendamento.Status != StatusAgendamento.EmAtendimento)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "Somente agendamentos em atendimento podem ser concluídos."
                    });
            }

            agendamento.Status = StatusAgendamento.Concluido;
            agendamento.DataAtualizacao = DateTime.UtcNow;

            return new ConcluirAtendimentoDto(
                agendamento.Id,
                agendamento.Status,
                agendamento.DataHoraInicio,
                agendamento.DataHoraFim,
                agendamento.ValorTotal);
        }
    }

}
