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
    public class ConfirmarAgendamentoHandler : ICommandHandler<ConfirmarAgendamentoCommand, ConfirmarAgendamentoDto>
    {

        private readonly IRepositorioGerenciarAgendamento _repositorio;
        private readonly ITenantContext _tenantContext;
        private readonly IUsuarioLogado _usuarioLogado;

        public ConfirmarAgendamentoHandler(
            IRepositorioGerenciarAgendamento repositorio,
            ITenantContext tenantContext,
            IUsuarioLogado usuarioLogado)
        {
            _repositorio = repositorio;
            _tenantContext = tenantContext;
            _usuarioLogado = usuarioLogado;
        }
        public async Task<ConfirmarAgendamentoDto> Handle(
            ConfirmarAgendamentoCommand command,
            CancellationToken cancellationToken)
        {
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw Validacao("Não foi possível identificar a empresa.");
            }

            if (!_usuarioLogado.IsAuthenticated)
            {
                throw Validacao("Usuário não autenticado.");
            }

            if (command.AgendamentoId == Guid.Empty)
            {
                throw Validacao("O identificador do agendamento é obrigatório.");
            }

            var agendamento =
                await _repositorio.ObterParaAtualizacaoAsync(
                    _tenantContext.EmpresaId,
                    command.AgendamentoId,
                    cancellationToken);

            if (agendamento is null)
            {
                throw Validacao(
                    "Agendamento não encontrado para a empresa informada.");
            }

            if (agendamento.Status != StatusAgendamento.Pendente)
            {
                throw Validacao(
                    $"Não é possível confirmar um agendamento com status " +
                    $"'{agendamento.Status}'. Somente agendamentos pendentes " +
                    "podem ser confirmados.");
            }

            agendamento.Status = StatusAgendamento.Confirmado;
            agendamento.DataAtualizacao = DateTime.UtcNow;

            return new ConfirmarAgendamentoDto(
                agendamento.Id,
                agendamento.Status,
                agendamento.DataHoraInicio,
                agendamento.DataHoraFim);
        }

        private static CustomValidationException Validacao(string mensagem)
        {
            return new CustomValidationException(
                new[] { mensagem });
        }
    }
}

