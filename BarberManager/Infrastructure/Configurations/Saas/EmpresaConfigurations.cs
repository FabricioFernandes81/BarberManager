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
    public class EmpresaConfigurations : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.HasKey(z => z.Id);
            builder.HasOne(x => x.Responsavel)
                .WithMany(x => x.Empresas)
                .HasForeignKey(x => x.ResponsavelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.RazaoSocial)
                .IsRequired(false);

            builder.Property(x => x.Cnpj)
                .IsRequired(false);
        }
    }


    public class EmpresaModuloConfigurations : IEntityTypeConfiguration<EmpresaModulo>
    {
        public void Configure(EntityTypeBuilder<EmpresaModulo> builder)
        {
            builder.HasKey(x => new
            {
                x.EmpresaId,
                x.ModuloId
            });

            builder.HasOne(x => x.Modulo)
                .WithMany(x=>x.EmpresaModulos)
                .HasForeignKey(x => x.ModuloId);

            builder.HasOne(e => e.Empresa)
                .WithMany(e => e.EmpresaModulos)
                .HasForeignKey(e => e.EmpresaId);
                
        }
    }
}
