using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Compras;
using Application.Operacional.Compras.DTOs;
using Domain.Entities.Compras;

namespace Application.Operacional.Compras.Commands.Handlers
{
    public class CriarFornecedorHandler
        : ICommandHandler<CriarFornecedorCommand, CriarFornecedorDto>
    {
        private readonly IRepositorioFornecedor _repositorio;
        private readonly ITenantContext _tenantContext;

        public CriarFornecedorHandler(
            IRepositorioFornecedor repositorio,
            ITenantContext tenantContext)
        {
            _repositorio = repositorio;
            _tenantContext = tenantContext;
        }

        public async Task<CriarFornecedorDto> Handle(
            CriarFornecedorCommand command,
            CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            // 1. Validar empresa autenticada.
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");
            }

            var empresaId = _tenantContext.EmpresaId;

            // 2. Normalizar os dados.
            var nome = command.Nome?.Trim() ?? string.Empty;

            var nomeFantasia = Normalizar(command.NomeFantasia);

            var documento = string.IsNullOrWhiteSpace(command.Documento)
                ? null
                : new string(
                    command.Documento
                        .Where(char.IsDigit)
                        .ToArray());

            var email = Normalizar(command.Email)?.ToLowerInvariant();

            var telefone = Normalizar(command.Telefone);

            var observacao = Normalizar(command.Observacao);

            // 3. Validar campos obrigatórios e limites.
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
                erros.Add(
                    "O e-mail deve ter no máximo 254 caracteres.");
            }

            if (telefone?.Length > 20)
            {
                erros.Add(
                    "O telefone deve ter no máximo 20 caracteres.");
            }

            if (observacao?.Length > 1000)
            {
                erros.Add(
                    "A observação deve ter no máximo 1000 caracteres.");
            }

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            // 4. Verificar duplicidade do documento na empresa.
            if (documento is not null)
            {
                var documentoExiste =
                    await _repositorio.DocumentoExisteAsync(
                        empresaId,
                        documento,
                        cancellationToken);

                if (documentoExiste)
                {
                    erros.Add(
                        "Já existe um fornecedor cadastrado com esse documento.");
                }
            }

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            // 5. Criar entidade de domínio.
            var fornecedor = new Fornecedor(
                empresaId: empresaId,
                nome: nome,
                nomeFantasia: nomeFantasia,
                documento: documento,
                email: email,
                telefone: telefone,
                observacao: observacao);

            // 6. Registrar no contexto transacional.
            await _repositorio.AdicionarAsync(
                fornecedor,
                cancellationToken);

            // 7. Retornar os dados cadastrados.
            return new CriarFornecedorDto(
                Id: fornecedor.Id,
                Nome: fornecedor.Nome,
                NomeFantasia: fornecedor.NomeFantasia,
                Documento: fornecedor.Documento,
                Email: fornecedor.Email,
                Telefone: fornecedor.Telefone,
                Observacao: fornecedor.Observacao,
                Ativo: fornecedor.Ativo,
                DataCadastro: fornecedor.DataCadastro);
        }

        private static string? Normalizar(string? valor)
        {
            return string.IsNullOrWhiteSpace(valor)
                ? null
                : valor.Trim();
        }
    }
}
