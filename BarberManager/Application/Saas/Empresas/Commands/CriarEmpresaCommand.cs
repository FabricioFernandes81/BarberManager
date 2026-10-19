using Application.Common.Transactions;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Empresas.Commands
{
    public class CriarEmpresaCommand : ITransactionalCommand
    {
        public string NomeResponsavel { get; init; } = null!;
        public string Telefone { get; init; } = null!;
        public string NomeBarbearia { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string Senha { get; init; } = null!;
        public Guid PlanoId { get; init; }
        public PeriodicidadeCobranca PeriodicidadeCobranca { get; init; }

        public TransactionContext TransactionContext => TransactionContext.SaaS;
    }
}
