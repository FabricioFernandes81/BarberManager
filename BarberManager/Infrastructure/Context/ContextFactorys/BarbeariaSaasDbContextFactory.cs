using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context.ContextFactorys
{
    public class BarbeariaSaasDbContextFactory : IDesignTimeDbContextFactory<BarbeariaSaasDbContext>
    {
        public BarbeariaSaasDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder =
              new DbContextOptionsBuilder<BarbeariaSaasDbContext>();

            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;
              Database=BarbeariaDb;
              Trusted_Connection=True;
              MultipleActiveResultSets=true;
              TrustServerCertificate=True");

            return new BarbeariaSaasDbContext(optionsBuilder.Options);

        }
    }
}
