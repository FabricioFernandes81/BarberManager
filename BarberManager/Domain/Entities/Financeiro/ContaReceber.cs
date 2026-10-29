using Domain.Entities.Clientes;
using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Vendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Financeiro
{
    public class ContaReceber : EntityBase
    {
        public Guid EmpresaId { get; private set; }

        public Guid? ClienteId { get; private set; }

        public Cliente? Cliente { get; private set; }

        public DateTime DataEmissao { get; private set; }

        public DateTime DataVencimento { get; private set; }

        public decimal Valor { get; private set; }

        public decimal ValorRecebido { get; private set; }

        public StatusContas Status { get; private set; }

        public string Descricao { get; private set; } = string.Empty;

        public Guid? VendaId { get; private set; }

        public Venda? Venda { get; private set; }

        public ICollection<Recebimento> Recebimentos { get; private set; }
            = new List<Recebimento>();

        // Construtor exigido pelo Entity Framework Core.
        protected ContaReceber()
        {
        }

        public ContaReceber(
            Guid empresaId,
            Guid? clienteId,
            DateTime dataEmissao,
            DateTime dataVencimento,
            decimal valor,
            string descricao)
        {
            if (empresaId == Guid.Empty)
                throw new ArgumentException(
                    "A empresa deve ser informada.",
                    nameof(empresaId));

            if (valor <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(valor),
                    "O valor da conta deve ser maior que zero.");

            if (dataVencimento.Date < dataEmissao.Date)
                throw new ArgumentException(
                    "O vencimento não pode ser anterior à emissão.");

            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException(
                    "A descrição da conta é obrigatória.",
                    nameof(descricao));

            EmpresaId = empresaId;
            ClienteId = clienteId;
            DataEmissao = dataEmissao;
            DataVencimento = dataVencimento;
            Valor = decimal.Round(valor, 2);
            ValorRecebido = 0;
            Descricao = descricao.Trim();
            Status = StatusContas.Aberta;
        }
        public void RegistrarRecebimento(
    decimal valor,
    DateTime dataRecebimento)
        {
            if (valor <= 0)
            {
                throw new InvalidOperationException(
                    "O valor do recebimento deve ser maior que zero.");
            }

            if (Status is StatusContas.Cancelada or StatusContas.Recebida)
            {
                throw new InvalidOperationException(
                    "Não é possível receber uma conta cancelada ou já liquidada.");
            }

            if (Valor <= 0 || ValorRecebido < 0 || ValorRecebido > Valor)
            {
                throw new InvalidOperationException(
                    "A conta possui valores financeiros inconsistentes.");
            }

            var saldoRestante = Valor - ValorRecebido;

            if (valor > saldoRestante)
            {
                throw new InvalidOperationException(
                    "O valor informado ultrapassa o saldo restante da conta.");
            }

            ValorRecebido = decimal.Round(
                ValorRecebido + valor,
                2,
                MidpointRounding.AwayFromZero);

            if (ValorRecebido == Valor)
            {
                Status = StatusContas.Recebida;
            }
            else if (DataVencimento.Date < dataRecebimento.Date)
            {
                // A conta continua vencida enquanto houver saldo pendente.
                Status = StatusContas.Vencida;
            }
            else
            {
                Status = StatusContas.Parcial;
            }
        }
        public decimal ObterSaldoRestante()
        {
            return Math.Max(Valor - ValorRecebido, 0);
        }

        public void VincularVenda(Guid vendaId)
        {
            if (vendaId == Guid.Empty)
                throw new ArgumentException(
                    "O identificador da venda é obrigatório.",
                    nameof(vendaId));

            if (VendaId.HasValue && VendaId.Value != vendaId)
                throw new InvalidOperationException(
                    "A conta a receber já está vinculada a outra venda.");

            VendaId = vendaId;
        }
    }

}
