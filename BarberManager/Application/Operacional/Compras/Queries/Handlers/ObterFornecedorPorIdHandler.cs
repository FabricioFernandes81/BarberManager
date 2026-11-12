using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Compras;
using Application.Operacional.Compras.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Compras.Queries.Handlers
{
    public class ObterFornecedorPorIdHandler : IQueryHandler<ObterFornecedorPorIdQuery, FornecedorDetalheDto>
    {
        private readonly ITenantContext _tenantContext;
    private readonly IRepositorioFornecedor _repositorio;

    public ObterFornecedorPorIdHandler(
        ITenantContext tenantContext,
        IRepositorioFornecedor repositorio)
    {
        _tenantContext = tenantContext;
        _repositorio = repositorio;
    }

    public async Task<FornecedorDetalheDto> Handle(
        ObterFornecedorPorIdQuery query,
        CancellationToken cancellationToken)
    {
        if (!_tenantContext.IsAvailable ||
            _tenantContext.EmpresaId == Guid.Empty)
        {
            throw new UnauthorizedAccessException(
                "Não foi possível identificar a empresa do usuário.");
        }

        if (query.FornecedorId == Guid.Empty)
        {
            throw new CustomValidationException(
                new[] { "O ID do fornecedor é inválido." });
        }

        var fornecedor = await _repositorio.ObterPorIdAsync(
            _tenantContext.EmpresaId,
            query.FornecedorId,
            cancellationToken);

        if (fornecedor is null)
        {
            throw new CustomValidationException(
                new[] { "Fornecedor não encontrado." });
        }

        return new FornecedorDetalheDto(
            fornecedor.Id,
            fornecedor.Nome,
            fornecedor.NomeFantasia,
            fornecedor.Documento,
            fornecedor.Email,
            fornecedor.Telefone,
            fornecedor.Observacao,
            fornecedor.Ativo,
            fornecedor.DataCadastro,
            fornecedor.DataAtualizacao, fornecedor.PoliticaFinanceiraDevolucao);
    }
}
}
