using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Common.Tenancy;
using Application.Interfaces.Agendamentos;
using Application.Interfaces.Vendas;
using Application.Operacional.Agendamentos.DTOs;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Vendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Agendamentos.Commands.Handlers
{
    public sealed class IniciarAtendimentoHandler
        : ICommandHandler<
            IniciarAtendimentoCommand,

            IniciarAtendimentoDto>
    {
        private readonly IRepositorioGerenciarAgendamento _repositorio;
        private readonly ITenantContext _tenantContext;
        private readonly IUsuarioLogado _usuarioLogado;
        private readonly IRepositorioVenda _repositorioVenda;

        public IniciarAtendimentoHandler(
            IRepositorioGerenciarAgendamento repositorio,
            ITenantContext tenantContext,
              IRepositorioVenda repositorioVenda,
            IUsuarioLogado usuarioLogado)
        {
            _repositorio = repositorio;
            _tenantContext = tenantContext;
            _repositorioVenda = repositorioVenda;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<IniciarAtendimentoDto> Handle(
            IniciarAtendimentoCommand command,
            CancellationToken cancellationToken)
        {
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw Validacao(
                    "Não foi possível identificar a empresa.");
            }

            if (!_usuarioLogado.IsAuthenticated)
            {
                throw Validacao("Usuário não autenticado.");
            }

            if (command.AgendamentoId == Guid.Empty)
            {
                throw Validacao(
                    "O identificador do agendamento é obrigatório.");
            }

            var empresaId = _tenantContext.EmpresaId;

            var agendamento =
                await _repositorio.ObterParaAtualizacaoAsync(
                    empresaId,
                    command.AgendamentoId,
                    cancellationToken);

            if (agendamento is null)
            {
                throw Validacao(
                    "Agendamento não encontrado para a empresa informada.");
            }

            if (agendamento.Status != StatusAgendamento.Confirmado &&
                agendamento.Status != StatusAgendamento.EmAtendimento)
            {
                throw Validacao(
                    "Somente agendamentos confirmados ou em atendimento podem abrir ou recuperar uma venda.");
            }

            // Recupera uma venda existente, se houver.
            var venda = await _repositorioVenda.ObterPorAgendamentoAsync(
                empresaId,
                agendamento.Id,
                cancellationToken);

            if (venda is null)
            {
                venda = new Venda(
                    empresaId: empresaId,
                    agendamentoId: agendamento.Id,
                    clienteId: agendamento.ClienteId,
                    profissionalId: agendamento.ProfissionalId,
                    usuarioResponsavelId: _usuarioLogado.UsuarioId,
                    dataAbertura: DateTimeOffset.UtcNow);

                foreach (var servico in agendamento.Servicos)
                {
                    var item = new VendaItem(
                        vendaId: venda.Id,
                        tipoItem: TipoItemVenda.Servico,
                        itemId: servico.ServicoId,
                        descricao: servico.NomeServico,
                        quantidade: 1m,
                        valorUnitario: servico.Valor,
                        desconto: 0m);

                    venda.AdicionarItem(item);
                }

                await _repositorioVenda.AdicionarAsync(
                    venda,
                    cancellationToken);
            }

            // Não regride o status se o atendimento já estiver em andamento.
            if (agendamento.Status == StatusAgendamento.Confirmado)
            {
                agendamento.Status = StatusAgendamento.EmAtendimento;
                agendamento.DataAtualizacao = DateTime.UtcNow;
            }

            return new IniciarAtendimentoDto(
                agendamento.Id,
                venda.Id,
                agendamento.Status,
                agendamento.DataHoraInicio,
                agendamento.DataHoraFim);
        }

        private static CustomValidationException Validacao(
            string mensagem)
        {
            return new CustomValidationException(
                new[] { mensagem });
        }
    }
}
