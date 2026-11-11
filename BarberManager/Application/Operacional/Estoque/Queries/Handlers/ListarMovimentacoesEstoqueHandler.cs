
using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Estoque;
using Application.Operacional.Estoque.DTOs;
using Application.Operacional.Estoque.Queries;
using Domain.Entities.Common.Enuns;

namespace Application.Operacional.Estoque.Queries.Handlers
{
    public class ListarMovimentacoesEstoqueHandler
        : IQueryHandler<
            ListarMovimentacoesEstoqueQuery,
            ListarMovimentacoesEstoqueDto>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IRepositorioEstoque _repositorio;

        public ListarMovimentacoesEstoqueHandler(
            ITenantContext tenantContext,
            IRepositorioEstoque repositorio)
        {
            _tenantContext = tenantContext;
            _repositorio = repositorio;
        }

        public async Task<ListarMovimentacoesEstoqueDto> Handle(
            ListarMovimentacoesEstoqueQuery query,
            CancellationToken cancellationToken)
        {
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");
            }

            var erros = new List<string>();

            if (query.ProdutoId == Guid.Empty)
            {
                erros.Add("O ID do produto é inválido.");
            }

            if (query.PageNumber < 1)
            {
                erros.Add("A página deve ser maior que zero.");
            }

            if (query.PageSize < 1 || query.PageSize > 100)
            {
                erros.Add(
                    "O tamanho da página deve estar entre 1 e 100.");
            }

            if (query.DataInicio.HasValue &&
                query.DataFim.HasValue &&
                query.DataInicio.Value > query.DataFim.Value)
            {
                erros.Add(
                    "A data inicial não pode ser maior que a data final.");
            }

            if (query.Tipo.HasValue &&
                !Enum.IsDefined(typeof(TipoMovimentacaoEstoque), query.Tipo.Value))
            {
                erros.Add("O tipo de movimentação informado é inválido.");
            }

            if (query.Origem.HasValue &&
                !Enum.IsDefined(typeof(OrigemMovimentacaoEstoque), query.Origem.Value))
            {
                erros.Add("A origem da movimentação informada é inválida.");
            }

            if (erros.Count > 0)
            {
                throw new CustomValidationException(erros);
            }

            var empresaId = _tenantContext.EmpresaId;

            var produtoExiste = await _repositorio.ProdutoExisteAsync(
                empresaId,
                query.ProdutoId,
                cancellationToken);

            if (!produtoExiste)
            {
                throw new CustomValidationException(
                    new[] { "Produto não encontrado." });
            }

            var (movimentacoes, totalRegistros) =
                await _repositorio.ListarMovimentacoesAsync(
                    empresaId,
                    query.ProdutoId,
                    query.DataInicio,
                    query.DataFim,
                    query.Tipo,
                    query.Origem,
                    query.PageNumber,
                    query.PageSize,
                    cancellationToken);

            var itens = movimentacoes
                .Select(m => new MovimentacaoEstoqueDto(
                    m.Id,
                    m.ProdutoId,
                    m.Tipo.ToString(),
                    m.Origem.ToString(),
                    m.Quantidade,
                    m.SaldoAnterior,
                    m.SaldoPosterior,
                    m.DocumentoOrigemId,
                    m.UsuarioResponsavelId,
                    m.DataMovimento,
                    m.Observacao))
                .ToArray();

            var totalPaginas = (int)Math.Ceiling(
                totalRegistros / (double)query.PageSize);

            return new ListarMovimentacoesEstoqueDto(
                query.ProdutoId,
                itens,
                query.PageNumber,
                query.PageSize,
                totalRegistros,
                totalPaginas);
        }
    }
}
