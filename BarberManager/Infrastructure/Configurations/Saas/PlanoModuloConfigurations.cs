using Domain.Entities.Saas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Saas
{
    public class PlanoModuloConfigurations : IEntityTypeConfiguration<PlanoModulo>
    {
        public void Configure(EntityTypeBuilder<PlanoModulo> builder)
        {
            builder.HasKey(x => new { x.PlanoId, x.ModuloId });

            builder.HasOne(x => x.Plano)
                .WithMany(x => x.Modulos)
                .HasForeignKey(x => x.PlanoId);


            builder.HasOne(x => x.Modulo)
                .WithMany()
                .HasForeignKey(x => x.ModuloId);

        }
    }
}
