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
    public class AssinaturaConfigurations : IEntityTypeConfiguration<Assinatura>
    {
        public void Configure(EntityTypeBuilder<Assinatura> builder)
        {
            builder.HasKey(x=> new {x.Id});

            builder.Property(x => x.DataInicio)
                .IsRequired();

            builder.Property(x => x.DataVencimento)
                .IsRequired();

            builder.Property(x => x.RenovacaoAutomatica);

            builder.Property(x => x.Trial);

            builder.Property(x => x.Ativa);

             builder.HasOne(x => x.Empresa)
                  .WithOne(x => x.Assinaturas)
                  .HasForeignKey<Assinatura>(x => x.EmpresaId);

            builder.HasOne(x => x.Plano)
                .WithOne(x=>x.Assinatura)
                .HasForeignKey<Assinatura>(x => x.PlanoId);
        }
    }
}
