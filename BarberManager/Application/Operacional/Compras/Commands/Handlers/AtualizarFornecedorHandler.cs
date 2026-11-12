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
    public class AtualizarFornecedorHandler
       : ICommandHandler<AtualizarFornecedorCommand, FornecedorDetalheDto>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IRepositorioFornecedor _repositorio;

        public AtualizarFornecedorHandler(
            ITenantContext tenantContext,
            IRepositorioFornecedor repositorio)
        {
            _tenantContext = tenantContext;
            _repositorio = repositorio;
        }

        public async Task<FornecedorDetalheDto> Handle(
            AtualizarFornecedorCommand command,
            CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            // 1. Validar o contexto da empresa.
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");
            }

            // 2. Validar identificador.
            if (command.FornecedorId == Guid.Empty)
            {
                erros.Add("O identificador do fornecedor é inválido.");
            }

            if (!Enum.IsDefined(command.PoliticaFinanceiraDevolucao))
            {
                erros.Add("A política financeira de devolução informada é inválida.");
            }

            // 3. Normalizar os dados.
            var nome = command.Nome?.Trim() ?? string.Empty;

            var nomeFantasia = Normalizar(command.NomeFantasia);

            var documento = NormalizarDocumento(command.Documento);

            var email = Normalizar(command.Email)?.ToLowerInvariant();

            var telefone = Normalizar(command.Telefone);

            var observacao = Normalizar(command.Observacao);

            // 4. Validar campos.
            if (string.IsNullOrWhiteSpace(nome))
            {
                erros.Add("O nome do fornecedor é obrigatório.");
            }
            else if (nome.Length > 150)
            {
                erros.Add(
                    "O nome do fornecedor deve ter no máximo 150 caracteres.");
            }

            if (nomeFantasia?.Length > 150)
            {
                erros.Add(
                    "O nome fantasia deve ter no máximo 150 caracteres.");
            }

            if (documento is not null &&
                documento.Length != 11 &&
                documento.Length != 14)
            {
                erros.Add(
                    "O documento deve possuir 11 dígitos (CPF) " +
                    "ou 14 dígitos (CNPJ).");
            }

            if (email?.Length > 254)
            {
                erros.Add("O e-mail deve ter no máximo 254 caracteres.");
            }

            if (telefone?.Length > 20)
            {
                erros.Add("O telefone deve ter no máximo 20 caracteres.");
            }

            if (observacao?.Length > 1000)
            {
                erros.Add(
                    "A observação deve ter no máximo 1000 caracteres.");
            }

            if (erros.Count > 0)
            {
                throw new CustomValidationException(erros);
            }

            cancellationToken.ThrowIfCancellationRequested();

            var empresaId = _tenantContext.EmpresaId;

            // 5. Buscar fornecedor rastreado dentro da empresa.
            var fornecedor = await _repositorio.ObterPorIdParaAtualizacaoAsync(
                empresaId,
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

            // 6. Verificar documento duplicado.
            if (documento is not null)
            {
                var documentoExiste =
                    await _repositorio.DocumentoExisteAsync(
                        empresaId,
                        documento,
                        cancellationToken,
                        fornecedor.Id);

                if (documentoExiste)
                {
                    erros.Add(
                        "Já existe outro fornecedor com esse documento.");
                }
            }

            if (erros.Count > 0)
            {
                throw new CustomValidationException(erros);
            }

            cancellationToken.ThrowIfCancellationRequested();

            // 7. Atualizar os dados pela entidade de domínio.
            fornecedor.AtualizarDados(
                nome,
                nomeFantasia,
                documento,
                email,
                telefone,
                observacao);

            fornecedor.AlterarPoliticaFinanceiraDevolucao(
    command.PoliticaFinanceiraDevolucao);
            // 8. A entidade está rastreada pelo DbContext.
            // O TransactionMiddleware fará a persistência.
            await _repositorio.AtualizarAsync(fornecedor);

            // 9. Retornar os dados atualizados.
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

        private static string? Normalizar(string? valor)
        {
            return string.IsNullOrWhiteSpace(valor)
                ? null
                : valor.Trim();
        }

        private static string? NormalizarDocumento(string? documento)
        {
            if (string.IsNullOrWhiteSpace(documento))
                return null;

            return new string(
                documento.Where(char.IsDigit).ToArray());
        }
    }
}
