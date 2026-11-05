using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Financeiro;
using Application.Operacional.Financeiro.DTOs;
using Domain.Entities.Common.Enuns;

namespace Application.Operacional.Financeiro.Queries.Handlers
{
    public sealed class ListarContasPagarHandler
        : IQueryHandler<ListarContasPagarQuery, ListarContasPagarDto>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IRepositorioContaPagar _repositorio;

        public ListarContasPagarHandler(
            ITenantContext tenantContext,
            IRepositorioContaPagar repositorio)
        {
            _tenantContext = tenantContext;
            _repositorio = repositorio;
        }

        public async Task<ListarContasPagarDto> Handle(
            ListarContasPagarQuery query,
            CancellationToken cancellationToken)
        {
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");
            }

            var erros = new List<string>();

            if (query.PageNumber < 1)
                erros.Add("A página deve ser maior que zero.");

            if (query.PageSize is < 1 or > 100)
            {
                erros.Add(
                    "O tamanho da página deve estar entre 1 e 100.");
            }

            if (query.DataInicio.HasValue &&
                query.DataInicio.Value == default)
            {
                erros.Add("A data inicial informada é inválida.");
            }

            if (query.DataFim.HasValue &&
                query.DataFim.Value == default)
            {
                erros.Add("A data final informada é inválida.");
            }

            if (query.DataInicio.HasValue &&
                query.DataFim.HasValue &&
                query.DataInicio.Value.Date > query.DataFim.Value.Date)
            {
                erros.Add(
                    "A data inicial não pode ser posterior à data final.");
            }

            if (query.FornecedorId.HasValue &&
                query.FornecedorId.Value == Guid.Empty)
            {
                erros.Add("O identificador do fornecedor é inválido.");
            }

            if (query.CompraId.HasValue &&
                query.CompraId.Value == Guid.Empty)
            {
                erros.Add("O identificador da compra é inválido.");
            }

            if (query.Status.HasValue &&
                !Enum.IsDefined(typeof(StatusContas), query.Status.Value))
            {
                erros.Add("O status informado é inválido.");
            }

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            var searchTerm = string.IsNullOrWhiteSpace(query.SearchTerm)
                ? null
                : query.SearchTerm.Trim();

            var dataReferencia = DateTime.UtcNow.Date;

            var (contas, totalRegistros) =
                await _repositorio.ListarAsync(
                    _tenantContext.EmpresaId,
                    query.PageNumber,
                    query.PageSize,
                    searchTerm,
                    query.DataInicio,
                    query.DataFim,
                    query.FornecedorId,
                    query.CompraId,
                    query.Status,
                    dataReferencia,
                    cancellationToken);

            var itens = contas
                .Select(c =>
                {
                    var status = ObterStatusExibicao(
                        c.Status,
                        c.DataVencimento,
                        dataReferencia);

                    return new ContaPagarListaDto(
                        c.Id,
                        c.FornecedorId,
                        c.Fornecedor.Nome,
                        c.CompraId,
                        c.Descricao,
                        c.DataEmissao,
                        c.DataVencimento,
                        c.Valor,
                        c.ValorPago,
                        Math.Max(0, c.Valor - c.ValorPago),
                        status,
                        c.NumeroParcela,
                        c.TotalParcelas);
                })
                .ToArray();

            var totalPaginas = (int)Math.Ceiling(
                totalRegistros / (double)query.PageSize);

            return new ListarContasPagarDto(
                itens,
                query.PageNumber,
                query.PageSize,
                totalRegistros,
                totalPaginas);
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