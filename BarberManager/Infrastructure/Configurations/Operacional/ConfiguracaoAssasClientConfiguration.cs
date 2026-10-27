using Domain.Entities.Financeiro;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Operacional
{
    public class ConfiguracaoAssasClientConfiguration : IEntityTypeConfiguration<ConfiguracaoAssasClient>
    {
        public void Configure(EntityTypeBuilder<ConfiguracaoAssasClient> builder)
        {
            builder.ToTable("ConfigurarcaoAssasCliente");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.EmpresaId)
                .IsUnique();
        }
    }
}
