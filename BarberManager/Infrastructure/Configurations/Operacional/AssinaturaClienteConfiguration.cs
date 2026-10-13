using Domain.Entities.Assinaturas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Operacional
{
    public class AssinaturaClienteConfiguration
    : IEntityTypeConfiguration<AssinaturaCliente>
    {
        public void Configure(EntityTypeBuilder<AssinaturaCliente> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmpresaId)
                .IsRequired();

            builder.Property(x => x.ClienteId)
                .IsRequired();

            builder.Property(x => x.PlanoAssinaturaId)
                .IsRequired();

            builder.Property(x => x.ValorContratado)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Periodicidade)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.DataInicio)
                .IsRequired();

            builder.Property(x => x.DataFimCicloAtual)
                .IsRequired();

            builder.Property(x => x.RenovacaoAutomatica)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(x => x.ReferenciaGateway)
                .HasMaxLength(200);

            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.ClienteId
            });

            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.Status,
                x.DataProximaCobranca
            });

            builder.HasIndex(x => x.ReferenciaGateway)
                .IsUnique()
                .HasFilter("[ReferenciaGateway] IS NOT NULL");

            builder.HasOne(x => x.PlanoAssinatura)
                .WithMany(x => x.Assinaturas)
                .HasForeignKey(x => x.PlanoAssinaturaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Beneficiarios)
                .WithOne(x => x.AssinaturaCliente)
                .HasForeignKey(x => x.AssinaturaClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Utilizacoes)
                .WithOne(x => x.AssinaturaCliente)
                .HasForeignKey(x => x.AssinaturaClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
