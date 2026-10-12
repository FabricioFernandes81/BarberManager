using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context.ContextFactorys
{
    public class AuthDbContextFactory
          : IDesignTimeDbContextFactory<AuthDbContext>
    {
        public AuthDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder =
               new DbContextOptionsBuilder<AuthDbContext>();

            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;
              Database=BarbeariaDb;
              Trusted_Connection=True;
              MultipleActiveResultSets=true;
              TrustServerCertificate=True");

            return new AuthDbContext(
                optionsBuilder.Options);

        }
    }
}
