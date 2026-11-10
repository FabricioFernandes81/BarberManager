using Domain.Entities.Financeiro;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Operacional
{
    public class CobrancaOperacionalConfiguration
        : IEntityTypeConfiguration<CobrancaOperacional>
    {
        public void Configure(
            EntityTypeBuilder<CobrancaOperacional> builder)
        {
            builder.ToTable("CobrancasOperacionais");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmpresaId)
                .IsRequired();

            builder.Property(x => x.ContaReceberId)
                .IsRequired();

            builder.Property(x => x.Gateway)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.ExternalReference)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.GatewayPagamentoId)
                .HasMaxLength(100);

            builder.Property(x => x.ChaveIdempotencia)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Valor)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.CriadaEmUtc)
                .IsRequired();

            builder.Property(x => x.ConfirmadaEmUtc);

            builder.HasOne(x => x.ContaReceber)
                .WithMany()
                .HasForeignKey(x => x.ContaReceberId)
                .OnDelete(DeleteBehavior.Restrict);

            // Idempotência por empresa.
            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.ChaveIdempotencia
            })
            .IsUnique()
            .HasDatabaseName(
                "UX_CobrancasOperacionais_Empresa_Idempotencia");

            // Uma cobrança do gateway não pode pertencer
            // a duas empresas ou registros internos.
            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.Gateway,
                x.GatewayPagamentoId
            })
            .IsUnique()
            .HasFilter("[GatewayPagamentoId] IS NOT NULL")
            .HasDatabaseName(
                "UX_CobrancasOperacionais_Gateway_Pagamento");

            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.ExternalReference
            })
            .IsUnique()
            .HasDatabaseName(
                "UX_CobrancasOperacionais_ExternalReference");

            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.ContaReceberId
            });

            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.Status
            });
            builder.Property(x => x.InvoiceUrl)
    .HasMaxLength(2048)
    .IsRequired(false);
        }
    }
}