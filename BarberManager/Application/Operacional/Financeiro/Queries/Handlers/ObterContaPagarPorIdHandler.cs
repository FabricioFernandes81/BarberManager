using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Financeiro;
using Application.Operacional.Financeiro.DTOs;
using Domain.Entities.Common.Enuns;

namespace Application.Operacional.Financeiro.Queries.Handlers
{
    public sealed class ObterContaPagarPorIdHandler
        : IQueryHandler<ObterContaPagarPorIdQuery, ContaPagarDetalheDto>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IRepositorioContaPagar _repositorio;

        public ObterContaPagarPorIdHandler(
            ITenantContext tenantContext,
            IRepositorioContaPagar repositorio)
        {
            _tenantContext = tenantContext;
            _repositorio = repositorio;
        }

        public async Task<ContaPagarDetalheDto> Handle(
            ObterContaPagarPorIdQuery query,
            CancellationToken cancellationToken)
        {
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");
            }

            if (query.ContaPagarId == Guid.Empty)
            {
                throw new CustomValidationException(
                    new[] { "O identificador da conta a pagar é inválido." });
            }

            var conta = await _repositorio.ObterDetalheAsync(
                _tenantContext.EmpresaId,
                query.ContaPagarId,
                cancellationToken);

            if (conta is null)
            {
                throw new CustomValidationException(
                    new[] { "Conta a pagar não encontrada." });
            }

            var dataReferencia = DateTime.UtcNow.Date;

            var status = ObterStatusExibicao(
                conta.Status,
                conta.DataVencimento,
                dataReferencia);

            var pagamentos = conta.Pagamentos
                .OrderBy(p => p.DataPagamento)
                .ThenBy(p => p.Id)
                .Select(p => new PagamentoContaPagarDto(
                    p.Id,
                    p.FormaPagamentoId,
                    p.FormaPagamento.Nome,
                    p.DataPagamento,
                    p.Valor,
                    p.Observacao))
                .ToArray();

            return new ContaPagarDetalheDto(
                conta.Id,
                conta.FornecedorId,
                conta.Fornecedor.Nome,
                conta.Fornecedor.Documento,
                conta.CompraId,
                conta.Descricao,
                conta.DataEmissao,
                conta.DataVencimento,
                conta.Valor,
                conta.ValorPago,
                Math.Max(0, conta.Valor - conta.ValorPago),
                status,
                conta.NumeroParcela,
                conta.TotalParcelas,
                conta.DataCadastro,
                conta.DataAtualizacao,
                pagamentos);
        }

        private static StatusContas ObterStatusExibicao(
            StatusContas status,
            DateTime dataVencimento,
            DateTime dataReferencia)
        {
            if ((status == StatusContas.Aberta ||
                 status == StatusContas.Parcial) &&
                dataVencimento.Date < dataReferencia)
            {
                return StatusContas.Vencida;
            }

            return status;
        }
    }
}