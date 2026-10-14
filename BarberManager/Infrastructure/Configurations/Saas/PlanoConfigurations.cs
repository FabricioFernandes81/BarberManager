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
    public class PlanoConfigurations : IEntityTypeConfiguration<Plano>
    {
        public void Configure(EntityTypeBuilder<Plano> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ValorMensal)
               .HasPrecision(18, 2);

            builder.Property(x => x.ValorAnual)
                .HasPrecision(18, 2);


            builder.Property(x => x.LimiteProfissionais);

            builder.Property(x => x.LimiteEmpresas);

           /* builder.Property(x => x.Ativo)
                .HasDefaultValue(true);*/

        }
    }
}
