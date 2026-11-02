using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Compras
{
    public class Fornecedor : EntityBase
    {
        public Guid EmpresaId { get; private set; }

        public string Nome { get; private set; } = string.Empty;

        public string? NomeFantasia { get; private set; }

        public string? Documento { get; private set; }

        public string? Email { get; private set; }

        public string? Telefone { get; private set; }

        public string? Observacao { get; private set; }

        public bool Ativo { get; private set; }

        public Fornecedor()
        {
        }

        public Fornecedor(
            Guid empresaId,
            string nome,
            string? nomeFantasia = null,
            string? documento = null,
            string? email = null,
            string? telefone = null,
            string? observacao = null)
        {
            if (empresaId == Guid.Empty)
                throw new ArgumentException(
                    "A empresa é obrigatória.",
                    nameof(empresaId));

            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException(
                    "O nome do fornecedor é obrigatório.",
                    nameof(nome));

            EmpresaId = empresaId;
            Nome = nome.Trim();
            NomeFantasia = Normalizar(nomeFantasia);
            Documento = NormalizarDocumento(documento);
            Email = Normalizar(email)?.ToLowerInvariant();
            Telefone = Normalizar(telefone);
            Observacao = Normalizar(observacao);

            Ativo = true;
        }

        public void AtualizarDados(
            string nome,
            string? nomeFantasia,
            string? documento,
            string? email,
            string? telefone,
            string? observacao)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException(
                    "O nome do fornecedor é obrigatório.",
                    nameof(nome));

            Nome = nome.Trim();
            NomeFantasia = Normalizar(nomeFantasia);
            Documento = NormalizarDocumento(documento);
            Email = Normalizar(email)?.ToLowerInvariant();
            Telefone = Normalizar(telefone);
            Observacao = Normalizar(observacao);

            DataAtualizacao = DateTime.UtcNow;
        }

        public void Ativar()
        {
            if (Ativo)
                return;

            Ativo = true;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void Desativar()
        {
            if (!Ativo)
                return;

            Ativo = false;
            DataAtualizacao = DateTime.UtcNow;
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
                documento
                    .Where(char.IsDigit)
                    .ToArray());
        }
    }
}
