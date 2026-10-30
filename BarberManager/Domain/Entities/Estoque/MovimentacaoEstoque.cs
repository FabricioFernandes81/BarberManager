using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Estoque
{
    public class MovimentacaoEstoque : EntityBase
    {
        public Guid EmpresaId { get; private set; }

        public Guid ProdutoId { get; private set; }

        public TipoMovimentacaoEstoque Tipo { get; private set; }

        public OrigemMovimentacaoEstoque Origem { get; private set; }

        public decimal Quantidade { get; private set; }

        public decimal SaldoAnterior { get; private set; }

        public decimal SaldoPosterior { get; private set; }

        public Guid? DocumentoOrigemId { get; private set; }

        public Guid? UsuarioResponsavelId { get; private set; }

        public DateTimeOffset DataMovimento { get; private set; }

        public string? Observacao { get; private set; }

        public MovimentacaoEstoque()
        {
        }

        public MovimentacaoEstoque(
            Guid empresaId,
            Guid produtoId,
            TipoMovimentacaoEstoque tipo,
            OrigemMovimentacaoEstoque origem,
            decimal quantidade,
            decimal saldoAnterior,
            decimal saldoPosterior,
            Guid usuarioResponsavelId,
            Guid? documentoOrigemId = null,
            string? observacao = null,
            DateTimeOffset? dataMovimento = null)
        {
            if (empresaId == Guid.Empty)
                throw new ArgumentException(
                    "A empresa é obrigatória.",
                    nameof(empresaId));

            if (produtoId == Guid.Empty)
                throw new ArgumentException(
                    "O produto é obrigatório.",
                    nameof(produtoId));

            if (usuarioResponsavelId == Guid.Empty)
                throw new ArgumentException(
                    "O usuário responsável é obrigatório.",
                    nameof(usuarioResponsavelId));

            if (quantidade <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(quantidade),
                    "A quantidade movimentada deve ser maior que zero.");

            if (saldoAnterior < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(saldoAnterior));

            if (saldoPosterior < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(saldoPosterior));

            if (!Enum.IsDefined(typeof(TipoMovimentacaoEstoque), tipo))
                throw new ArgumentOutOfRangeException(nameof(tipo));

            if (!Enum.IsDefined(typeof(OrigemMovimentacaoEstoque), origem))
                throw new ArgumentOutOfRangeException(nameof(origem));

            ValidarConsistencia(
                tipo,
                quantidade,
                saldoAnterior,
                saldoPosterior);

            EmpresaId = empresaId;
            ProdutoId = produtoId;

            Tipo = tipo;
            Origem = origem;

            Quantidade = quantidade;
            SaldoAnterior = saldoAnterior;
            SaldoPosterior = saldoPosterior;

            UsuarioResponsavelId = usuarioResponsavelId;
            DocumentoOrigemId = documentoOrigemId;

            Observacao = observacao?.Trim();

            DataMovimento = dataMovimento ?? DateTimeOffset.UtcNow;
        }

        private static void ValidarConsistencia(
            TipoMovimentacaoEstoque tipo,
            decimal quantidade,
            decimal saldoAnterior,
            decimal saldoPosterior)
        {
            var saldoEsperado = tipo switch
            {
                TipoMovimentacaoEstoque.Entrada =>
                    saldoAnterior + quantidade,

                TipoMovimentacaoEstoque.Saida =>
                    saldoAnterior - quantidade,

                TipoMovimentacaoEstoque.Ajuste =>
                    saldoPosterior,

                _ => throw new ArgumentOutOfRangeException(nameof(tipo))
            };

            if (tipo == TipoMovimentacaoEstoque.Saida &&
                quantidade > saldoAnterior)
            {
                throw new InvalidOperationException(
                    "A saída não pode superar o saldo anterior.");
            }

            if (tipo != TipoMovimentacaoEstoque.Ajuste &&
                saldoEsperado != saldoPosterior)
            {
                throw new InvalidOperationException(
                    "O saldo posterior é incompatível com a movimentação.");
            }

            if (tipo == TipoMovimentacaoEstoque.Ajuste &&
                saldoAnterior == saldoPosterior)
            {
                throw new InvalidOperationException(
                    "O ajuste precisa alterar o saldo do estoque.");
            }

            if (tipo == TipoMovimentacaoEstoque.Ajuste &&
                quantidade != Math.Abs(saldoPosterior - saldoAnterior))
            {
                throw new InvalidOperationException(
                    "A quantidade do ajuste deve corresponder à diferença entre os saldos.");
            }
        }
    }
}
