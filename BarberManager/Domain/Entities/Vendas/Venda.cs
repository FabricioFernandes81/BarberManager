using Domain.Entities.Agendamentos;
using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Vendas
{
    public class Venda : EntityBase
    {
        public Guid EmpresaId { get; private set; }

        public Guid? AgendamentoId { get; private set; }

        public Agendamento? Agendamento { get; private set; }

        public Guid? ClienteId { get; private set; }

        public Guid? ProfissionalId { get; private set; }

        public Guid UsuarioResponsavelId { get; private set; }

        public DateTimeOffset DataAbertura { get; private set; }

        public DateTimeOffset? DataFinalizacao { get; private set; }

        public StatusVenda Status { get; private set; }

        public decimal Subtotal { get; private set; }

        public decimal Desconto { get; private set; }

        public decimal ValorTotal { get; private set; }

        public ICollection<VendaItem> Itens { get; private set; }
            = new List<VendaItem>();

        protected Venda()
        {
        }

        public Venda(
            Guid empresaId,
            Guid? agendamentoId,
            Guid? clienteId,
            Guid? profissionalId,
            Guid usuarioResponsavelId,
            DateTimeOffset dataAbertura)
        {
            if (empresaId == Guid.Empty)
                throw new ArgumentException(
                    "A empresa é obrigatória.",
                    nameof(empresaId));

            if (usuarioResponsavelId == Guid.Empty)
                throw new ArgumentException(
                    "O usuário responsável é obrigatório.",
                    nameof(usuarioResponsavelId));

            if (agendamentoId == Guid.Empty)
                throw new ArgumentException(
                    "O agendamento informado é inválido.",
                    nameof(agendamentoId));

            EmpresaId = empresaId;
            AgendamentoId = agendamentoId;
            ClienteId = clienteId;
            ProfissionalId = profissionalId;
            UsuarioResponsavelId = usuarioResponsavelId;

            DataAbertura = dataAbertura;
            Status = StatusVenda.Aberta;
        }
    }
}
