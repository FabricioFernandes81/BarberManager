using Domain.Entities.Financeiro;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds.Operacional.Financeiro
{
    public static class SeedersFormaPagamento
    {
        private static readonly Guid FomaPagamento1 = Guid.Parse("dcd94a5b-ad96-4e1b-9954-ba233c7f8b9e");

        public static void Inicializer(ModelBuilder builder)
        {

            builder.Entity<FormaPagamento>().HasData(SetFormaPagamento());

        }

        private static List<FormaPagamento> SetFormaPagamento()
        {
            return new List<FormaPagamento>
            {
                new()
                {

                }
            };
        }
    }
}
