
using Domain.Entities.Compras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Operacional.Compras
{
    public class RecebimentoCompraConfiguration
        : IEntityTypeConfiguration<RecebimentoCompra>
    {
        public void Configure(
            EntityTypeBuilder<RecebimentoCompra> builder)
        {
            builder.ToTable("RecebimentosCompras");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.EmpresaId)
                .IsRequired();

            builder.Property(r => r.CompraId)
                .IsRequired();

            builder.Property(r => r.UsuarioResponsavelId)
                .IsRequired();

            builder.Property(r => r.DataRecebimento)
                .IsRequired();

            builder.Property(r => r.ChaveIdempotencia)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(r => r.Observacao)
                .HasMaxLength(500);

            builder.HasOne(r => r.Compra)
                .WithMany(c => c.Recebimentos)
                .HasForeignKey(r => r.CompraId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(r => r.Itens)
                .WithOne(i => i.RecebimentoCompra)
                .HasForeignKey(i => i.RecebimentoCompraId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(r => new
            {
                r.EmpresaId,
                r.ChaveIdempotencia
            })
            .IsUnique()
            .HasDatabaseName(
                "UX_RecebimentosCompras_Empresa_ChaveIdempotencia");

            builder.HasIndex(r => new
            {
                r.EmpresaId,
                r.CompraId,
                r.DataRecebimento
            });
        }
    }
}
