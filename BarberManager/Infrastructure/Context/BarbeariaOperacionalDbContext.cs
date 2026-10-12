using Domain.Entities.Profissionais;
using Infrastructure.Configurations.Operacional;
using Infrastructure.Seeds.Operacional;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class BarbeariaOperacionalDbContext : DbContext
    {

        public BarbeariaOperacionalDbContext(DbContextOptions<BarbeariaOperacionalDbContext> options)
            : base(options)
        {

        }

        public DbSet<Profissional> Profissionais { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProfissionalConfiguration());

            SeedProfissionais.SeedProfissial(modelBuilder);
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

    }
}
