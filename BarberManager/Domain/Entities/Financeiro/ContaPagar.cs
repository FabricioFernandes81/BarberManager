using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Financeiro
{
    public class ContaPagar : EntityBase
    {
        public Guid EmpresaId { get; set; }

        public Guid FornecedorId { get; set; }

        public Fornecedor Fornecedor { get; set; } = null!;

        public DateTime DataEmissao { get; set; }

        public DateTime DataVencimento { get; set; }

        public decimal Valor { get; set; }

        public decimal ValorPago { get; set; }

        public StatusContas Status { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public Guid? CompraId { get; set; }
        public Compra? Compra { get; set; }

        public int NumeroParcela { get; set; }

        public int TotalParcelas { get; set; }


        public ICollection<Pagamento> Pagamentos { get; set; }
            = new List<Pagamento>();

        public decimal ValorAbatidoDevolucao { get; private set; } = 0;


        public void RegistrarPagamento(
            decimal valor,
            DateTime dataPagamento)
        {
            if (valor <= 0)
            {
                throw new InvalidOperationException(
                    "O valor do pagamento deve ser maior que zero.");
            }

            if (valor != decimal.Round(valor, 2))
            {
                throw new InvalidOperationException(
                    "O valor do pagamento deve possuir no máximo duas casas decimais.");
            }

            if (Status is StatusContas.Cancelada or StatusContas.Recebida)
            {
                throw new InvalidOperationException(
                    "Não é possível pagar uma conta cancelada ou já liquidada.");
            }

            if (Valor <= 0 || ValorPago < 0 || ValorPago > Valor)
            {
                throw new InvalidOperationException(
                    "A conta possui valores financeiros inconsistentes.");
            }

            var saldoRestante = Valor - ValorPago;

            if (valor > saldoRestante)
            {
                throw new InvalidOperationException(
                    "O valor informado ultrapassa o saldo pendente da conta.");
            }

            ValorPago = decimal.Round(
                ValorPago + valor,
                2,
                MidpointRounding.AwayFromZero);

            if (ValorPago == Valor)
            {
                Status = StatusContas.Recebida;
            }
            else if (DataVencimento.Date < dataPagamento.Date)
            {
                Status = StatusContas.Vencida;
            }
            else
            {
                Status = StatusContas.Parcial;
            }

            DataAtualizacao = DateTime.UtcNow;
        }

        public decimal ObterSaldoRestante()
        {
            return Math.Max(
          0,
          Valor - ValorPago - ValorAbatidoDevolucao);
        }

        public void Cancelar()
        {
            if (Status == StatusContas.Cancelada)
                throw new InvalidOperationException(
                    "Esta conta a pagar já está cancelada.");

            if (Status == StatusContas.Parcial ||
                Status == StatusContas.Recebida ||
                ValorPago > 0 ||
                Pagamentos.Any())
            {
                throw new InvalidOperationException(
                    "Não é possível cancelar uma conta a pagar que possui pagamentos registrados.");
            }

            if (Status != StatusContas.Aberta &&
                Status != StatusContas.Vencida)
            {
                throw new InvalidOperationException(
                    "A situação atual da conta a pagar não permite o cancelamento.");
            }

            Status = StatusContas.Cancelada;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void RegistrarAbatimentoDevolucao(decimal valor)
        {
            if (valor <= 0)
                throw new InvalidOperationException(
                    "O valor do abatimento deve ser maior que zero.");

            if (valor != decimal.Round(valor, 2))
                throw new InvalidOperationException(
                    "O valor do abatimento deve possuir no máximo duas casas decimais.");

            if (Status == StatusContas.Cancelada)
                throw new InvalidOperationException(
                    "Não é possível abater uma conta cancelada.");

            var saldoRestante = ObterSaldoRestante();

            if (valor > saldoRestante)
                throw new InvalidOperationException(
                    "O abatimento ultrapassa o saldo pendente da conta.");

            ValorAbatidoDevolucao = decimal.Round(
                ValorAbatidoDevolucao + valor,
                2,
                MidpointRounding.AwayFromZero);

            DataAtualizacao = DateTime.UtcNow;
        }

    }

}
