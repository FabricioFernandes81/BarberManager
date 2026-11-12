
using Domain.Entities.Compras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Operacional.Compras
{
    public class DevolucaoCompraConfiguration
        : IEntityTypeConfiguration<DevolucaoCompra>
    {
        public void Configure(EntityTypeBuilder<DevolucaoCompra> builder)
        {
            builder.ToTable("DevolucoesCompras");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.EmpresaId)
                .IsRequired();

            builder.Property(d => d.CompraId)
                .IsRequired();

            builder.Property(d => d.UsuarioResponsavelId)
                .IsRequired();

            builder.Property(d => d.ChaveIdempotencia)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(d => d.DataDevolucao)
                .IsRequired();

            builder.Property(d => d.Motivo)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(d => d.Observacao)
                .HasMaxLength(500);

            builder.HasOne(d => d.Compra)
                .WithMany()
                .HasForeignKey(d => d.CompraId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Itens)
                .WithOne(i => i.DevolucaoCompra)
                .HasForeignKey(i => i.DevolucaoCompraId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(d => new
            {
                d.EmpresaId,
                d.ChaveIdempotencia
            })
            .IsUnique()
            .HasDatabaseName(
                "UX_DevolucoesCompras_Empresa_ChaveIdempotencia");

            builder.HasIndex(d => new
            {
                d.EmpresaId,
                d.CompraId,
                d.DataDevolucao
            });

            builder.Property(d => d.PoliticaFinanceiraDevolucao)
    .HasConversion<int>()
    .IsRequired();
        }
    }
}
