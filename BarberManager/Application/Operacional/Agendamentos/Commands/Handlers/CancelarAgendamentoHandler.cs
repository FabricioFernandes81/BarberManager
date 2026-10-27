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
    public class CancelarAgendamentoHandler : ICommandHandler<CancelarAgendamentoCommand, CancelarAgendamentoDto>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IUsuarioLogado _usuarioLogado;
        private readonly IRepositorioGerenciarAgendamento _repositorio;


        public CancelarAgendamentoHandler(ITenantContext tenantContext, IUsuarioLogado usuarioLogado, IRepositorioGerenciarAgendamento repositorio)
        {
            _repositorio = repositorio;
            _tenantContext = tenantContext;
            _usuarioLogado = usuarioLogado;
        }


        public async Task<CancelarAgendamentoDto> Handle(CancelarAgendamentoCommand command, CancellationToken cancellationToken)
        {
            // 1. Validar o contexto da empresa.

            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");
            }

            // 2. Validar o usuário autenticado.

            if (!_usuarioLogado.IsAuthenticated)
            {
                throw new UnauthorizedAccessException(
                    "É necessário estar autenticado para cancelar o agendamento.");
            }

            // 3. Validar o identificador.

            if (command.AgendamentoId == Guid.Empty)
            {
                throw Erro("O identificador do agendamento é obrigatório.");
            }

            // 4. Validar o motivo, quando informado.

            var motivo = command.Motivo?.Trim();

            if (motivo is { Length: > 500 })
            {
                throw Erro(
                    "O motivo do cancelamento não pode ultrapassar 500 caracteres.");
            }

            var empresaId = _tenantContext.EmpresaId;

            // 5. Buscar o agendamento da empresa autenticada.

            var agendamento =
                await _repositorio.ObterParaAtualizacaoAsync(
                    empresaId,
                    command.AgendamentoId,
                    cancellationToken);

            if (agendamento is null)
            {
                throw Erro(
                    "Agendamento não encontrado para a empresa.");
            }

            // 6. Validar a transição de status.

            if (agendamento.Status is not (
                StatusAgendamento.Pendente or
                StatusAgendamento.Confirmado))
            {
                throw Erro(
                    $"Não é possível cancelar um agendamento com status " +
                    $"'{agendamento.Status}'.");
            }

            // 7. Registrar o cancelamento.

            var dataCancelamento = DateTimeOffset.UtcNow;

            agendamento.Status = StatusAgendamento.Cancelado;
            agendamento.DataCancelamento = dataCancelamento;
            agendamento.MotivoCancelamento = motivo;
            agendamento.CanceladoPorUsuarioId = _usuarioLogado.UsuarioId;
            agendamento.DataAtualizacao = DateTime.UtcNow;

            // Não chamar SaveChangesAsync.
            // O TransactionMiddleware é responsável pela persistência.

            return new CancelarAgendamentoDto
            {
             AgendamentoId =agendamento.Id,
             Status = agendamento.Status,
             DataCancelamento = dataCancelamento,
             MotivoCancelamento =agendamento.MotivoCancelamento,
             CanceladoPorUsuarioId =  agendamento.CanceladoPorUsuarioId
            };
                
        }

        private static CustomValidationException Erro(string mensagem)
            => new(new[] { mensagem });
    }
}

