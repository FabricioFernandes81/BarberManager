using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Estoque
{
    public class EstoqueProduto : EntityBase
    {
        public Guid EmpresaId { get; private set; }

        public Guid ProdutoId { get; private set; }

        public decimal QuantidadeAtual { get; private set; }

        public decimal EstoqueMinimo { get; private set; }

        public DateTime DataAtualizacaoEstoque { get; private set; }

        public EstoqueProduto()
        {
        }

        public EstoqueProduto(
            Guid empresaId,
            Guid produtoId,
            decimal estoqueMinimo = 0)
        {
            if (empresaId == Guid.Empty)
                throw new ArgumentException(
                    "A empresa é obrigatória.",
                    nameof(empresaId));

            if (produtoId == Guid.Empty)
                throw new ArgumentException(
                    "O produto é obrigatório.",
                    nameof(produtoId));

            if (estoqueMinimo < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(estoqueMinimo));

            EmpresaId = empresaId;
            ProdutoId = produtoId;

            QuantidadeAtual = 0;
            EstoqueMinimo = estoqueMinimo;

            DataAtualizacaoEstoque = DateTime.UtcNow;
        }

        public void DefinirEstoqueMinimo(decimal quantidade)
        {
            if (quantidade < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(quantidade));

            EstoqueMinimo = quantidade;
            DataAtualizacaoEstoque = DateTime.UtcNow;
            DataAtualizacao = DateTime.UtcNow;
        }

        public decimal RegistrarEntrada(decimal quantidade)
        {
            ValidarQuantidade(quantidade);

            var saldoAnterior = QuantidadeAtual;

            QuantidadeAtual += quantidade;

            AtualizarData();

            return saldoAnterior;
        }

        public decimal RegistrarSaida(decimal quantidade)
        {
            ValidarQuantidade(quantidade);

            if (quantidade > QuantidadeAtual)
                throw new InvalidOperationException(
                    "Estoque insuficiente para realizar a saída.");

            var saldoAnterior = QuantidadeAtual;

            QuantidadeAtual -= quantidade;

            AtualizarData();

            return saldoAnterior;
        }

        public decimal AjustarSaldo(decimal novoSaldo)
        {
            if (novoSaldo < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(novoSaldo),
                    "O saldo não pode ser negativo.");

            var saldoAnterior = QuantidadeAtual;

            QuantidadeAtual = novoSaldo;

            AtualizarData();

            return saldoAnterior;
        }

        private static void ValidarQuantidade(decimal quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(quantidade),
                    "A quantidade deve ser maior que zero.");
        }

        private void AtualizarData()
        {
            DataAtualizacaoEstoque = DateTime.UtcNow;
            DataAtualizacao = DateTime.UtcNow;
        }
    }
}
