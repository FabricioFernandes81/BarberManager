using Application.Interfaces.Compras;
using Domain.Entities.Compras;
using Domain.Entities.Financeiro;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Compras
{
    public class RepositorioCompra : IRepositorioCompra
    {
        private readonly BarbeariaOperacionalDbContext _context;

        public RepositorioCompra(
            BarbeariaOperacionalDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(
            Compra compra,
            IReadOnlyCollection<ContaPagar> contasPagar,
            CancellationToken cancellationToken)
        {
            await _context.Compras.AddAsync(
                compra,
                cancellationToken);

            if (contasPagar.Count > 0)
            {
                await _context.ContasPagar.AddRangeAsync(
                    contasPagar,
                    cancellationToken);
            }

            // Não chamar SaveChangesAsync aqui.
            // O TransactionMiddleware confirma a transação.
        }
    }
}
