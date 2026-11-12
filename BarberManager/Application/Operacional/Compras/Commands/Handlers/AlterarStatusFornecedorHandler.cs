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

namespace Application.Operacional.Compras.Commands.Handlers
{
    public sealed class AlterarStatusFornecedorHandler
         : ICommandHandler<
             AlterarStatusFornecedorCommand,
             FornecedorDetalheDto>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IRepositorioFornecedor _repositorio;

        public AlterarStatusFornecedorHandler(
            ITenantContext tenantContext,
            IRepositorioFornecedor repositorio)
        {
            _tenantContext = tenantContext;
            _repositorio = repositorio;
        }

        public async Task<FornecedorDetalheDto> Handle(
            AlterarStatusFornecedorCommand command,
            CancellationToken cancellationToken)
        {
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");
            }

            if (command.FornecedorId == Guid.Empty)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "O identificador do fornecedor é inválido."
                    });
            }

            cancellationToken.ThrowIfCancellationRequested();

            var fornecedor =
                await _repositorio.ObterPorIdParaAtualizacaoAsync(
                    _tenantContext.EmpresaId,
                    command.FornecedorId,
                    cancellationToken);

            if (fornecedor is null)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "Fornecedor não encontrado nesta empresa."
                    });
            }

            if (command.Ativo)
                fornecedor.Ativar();
            else
                fornecedor.Desativar();

            await _repositorio.AtualizarAsync(fornecedor);

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
                fornecedor.DataAtualizacao,
                fornecedor.PoliticaFinanceiraDevolucao);
        }
    }
}
