using Domain.Entities.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Operacional
{
    public class ClienteEmpresaConfiguration : IEntityTypeConfiguration<ClienteEmpresa>
    {
        public void Configure(EntityTypeBuilder<ClienteEmpresa> builder)
        {
            builder.ToTable("ClientesEmpresas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ClienteId)
                .IsRequired();

            builder.Property(x => x.EmpresaId)
                .IsRequired();

            builder.Property(x => x.DataVinculo)
                .IsRequired();

            builder.Property(x => x.ObservacoesInternas)
                .HasMaxLength(1000);

            builder.Property(x => x.Ativo)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasOne(x => x.Cliente)
                .WithMany(x => x.Empresas)
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.ClienteId,
                x.EmpresaId
            })
            .IsUnique();

            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.Ativo
            });
        }
    }
    
}
