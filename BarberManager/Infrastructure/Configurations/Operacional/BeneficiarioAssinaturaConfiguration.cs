using Domain.Entities.Assinaturas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Operacional
{
    public class BeneficiarioAssinaturaConfiguration
    : IEntityTypeConfiguration<BeneficiarioAssinatura>
    {
        public void Configure(
            EntityTypeBuilder<BeneficiarioAssinatura> builder)
        {
            builder.ToTable("BeneficiariosAssinatura");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmpresaId)
                .IsRequired();

            builder.Property(x => x.AssinaturaClienteId)
                .IsRequired();

            builder.Property(x => x.ClienteId)
                .IsRequired();

            builder.Property(x => x.Titular)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(x => x.Ativo)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(x => x.DataVinculo)
                .IsRequired();

            builder.HasIndex(x => new
            {
                x.AssinaturaClienteId,
                x.ClienteId
            }).IsUnique();

            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.ClienteId,
                x.Ativo
            });

            builder.HasOne(x => x.AssinaturaCliente)
                .WithMany(x => x.Beneficiarios)
                .HasForeignKey(x => x.AssinaturaClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

