using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context.ContextFactorys
{
    public class BarbeariaOperacionalDbContextFactory : IDesignTimeDbContextFactory<BarbeariaOperacionalDbContext>

    {
        public BarbeariaOperacionalDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder =
                 new DbContextOptionsBuilder<BarbeariaOperacionalDbContext>();

            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;
              Database=BarbeariaDb;
              Trusted_Connection=True;
              MultipleActiveResultSets=true;
              TrustServerCertificate=True");

            return new BarbeariaOperacionalDbContext(
                optionsBuilder.Options);

        }
    }
}
