using Domain.Entities.Common;
using Domain.Entities.Estoque;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Produtos
{
    public class Produto : EntityBase
    {
        public Guid EmpresaId { get; private set; }

        public Guid? CategoriaProdutoId { get; private set; }

        public CategoriaProduto? CategoriaProduto { get; private set; }

        public Guid? MarcaId { get; private set; }

        public Marca? Marca { get; private set; }

        public string Nome { get; private set; } = string.Empty;

        public string? Descricao { get; private set; }

        public string? CodigoInterno { get; private set; }

        public string? CodigoBarras { get; private set; }

        public decimal ValorVenda { get; private set; }

        public decimal Custo { get; private set; }

        public bool ControlaEstoque { get; private set; }

        public bool Ativo { get; private set; }

        public EstoqueProduto? Estoque { get; private set; }

        public ICollection<MovimentacaoEstoque> MovimentacoesEstoque
        { get; private set; }
            = new List<MovimentacaoEstoque>();

        protected Produto()
        {
        }

        public Produto(
            Guid empresaId,
            string nome,
            decimal valorVenda,
            decimal custo,
            bool controlaEstoque,
            Guid? categoriaProdutoId = null,
            Guid? marcaId = null,
            string? descricao = null,
            string? codigoInterno = null,
            string? codigoBarras = null)
        {
            if (empresaId == Guid.Empty)
                throw new ArgumentException(
                    "A empresa é obrigatória.",
                    nameof(empresaId));

            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException(
                    "O nome do produto é obrigatório.",
                    nameof(nome));

            if (valorVenda < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(valorVenda),
                    "O valor de venda não pode ser negativo.");

            if (custo < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(custo),
                    "O custo não pode ser negativo.");

            if (categoriaProdutoId == Guid.Empty)
                throw new ArgumentException(
                    "A categoria informada é inválida.",
                    nameof(categoriaProdutoId));

            if (marcaId == Guid.Empty)
                throw new ArgumentException(
                    "A marca informada é inválida.",
                    nameof(marcaId));

            EmpresaId = empresaId;
            Nome = nome.Trim();
            ValorVenda = ArredondarMoeda(valorVenda);
            Custo = ArredondarMoeda(custo);
            ControlaEstoque = controlaEstoque;

            CategoriaProdutoId = categoriaProdutoId;
            MarcaId = marcaId;

            Descricao = descricao?.Trim();
            CodigoInterno = codigoInterno?.Trim();
            CodigoBarras = codigoBarras?.Trim();

            Ativo = true;
        }

        public void AtualizarDados(
            string nome,
            string? descricao,
            Guid? categoriaProdutoId,
            Guid? marcaId,
            string? codigoInterno,
            string? codigoBarras)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException(
                    "O nome do produto é obrigatório.",
                    nameof(nome));

            if (categoriaProdutoId == Guid.Empty)
                throw new ArgumentException(
                    "A categoria informada é inválida.",
                    nameof(categoriaProdutoId));

            if (marcaId == Guid.Empty)
                throw new ArgumentException(
                    "A marca informada é inválida.",
                    nameof(marcaId));

            Nome = nome.Trim();
            Descricao = descricao?.Trim();
            CategoriaProdutoId = categoriaProdutoId;
            MarcaId = marcaId;
            CodigoInterno = codigoInterno?.Trim();
            CodigoBarras = codigoBarras?.Trim();

            DataAtualizacao = DateTime.UtcNow;
        }

        public void AtualizarValores(
            decimal valorVenda,
            decimal custo)
        {
            if (valorVenda < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(valorVenda));

            if (custo < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(custo));

            ValorVenda = ArredondarMoeda(valorVenda);
            Custo = ArredondarMoeda(custo);

            DataAtualizacao = DateTime.UtcNow;
        }

        public void Desativar()
        {
            Ativo = false;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void Ativar()
        {
            Ativo = true;
            DataAtualizacao = DateTime.UtcNow;
        }

        private static decimal ArredondarMoeda(decimal valor)
        {
            return decimal.Round(
                valor,
                2,
                MidpointRounding.AwayFromZero);
        }
    }
}
