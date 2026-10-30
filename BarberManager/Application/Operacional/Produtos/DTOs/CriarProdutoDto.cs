using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Produtos.DTOs
{
    public sealed record CriarProdutoDto
    {
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(150,
            ErrorMessage = "O nome do produto deve ter no máximo 150 caracteres.")]
        public string Nome { get; init; } = string.Empty;

        [StringLength(100)]
        public string? Descricao { get; init; }

        [StringLength(50)]
        public string? CodigoInterno { get; init; }

        [StringLength(50)]
        public string? CodigoBarras { get; init; }

        public Guid? CategoriaProdutoId { get; init; }

        public Guid? MarcaId { get; init; }

        [Range(typeof(decimal),
            "0",
            "999999999",
            ErrorMessage = "O valor de venda não pode ser negativo.")]
        public decimal ValorVenda { get; init; }

        [Range(typeof(decimal),
            "0",
            "999999999",
            ErrorMessage = "O custo não pode ser negativo.")]
        public decimal Custo { get; init; }

        public bool ControlaEstoque { get; init; }

        [Range(typeof(decimal),
            "0",
            "999999999",
            ErrorMessage = "A quantidade inicial não pode ser negativa.")]
        public decimal QuantidadeEstoqueInicial { get; init; }

        [Range(
            typeof(decimal),
            "0",
            "999999999",
            ErrorMessage = "O estoque mínimo não pode ser negativo.")]
        public decimal EstoqueMinimo { get; init; }
    }
}
