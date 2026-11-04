
using Domain.Entities.Compras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Operacional.Compras
{
    public class RecebimentoCompraItemConfiguration
        : IEntityTypeConfiguration<RecebimentoCompraItem>
    {
        public void Configure(
            EntityTypeBuilder<RecebimentoCompraItem> builder)
        {
            builder.ToTable("RecebimentosComprasItens");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.RecebimentoCompraId)
                .IsRequired();

            builder.Property(i => i.CompraItemId)
                .IsRequired();

            builder.Property(i => i.ProdutoId)
                .IsRequired();

            builder.Property(i => i.QuantidadeRecebida)
                .HasPrecision(18, 3)
                .IsRequired();

            builder.HasOne(i => i.RecebimentoCompra)
                .WithMany(r => r.Itens)
                .HasForeignKey(i => i.RecebimentoCompraId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.CompraItem)
                .WithMany()
                .HasForeignKey(i => i.CompraItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Domain.Entities.Produtos.Produto>()
                .WithMany()
                .HasForeignKey(i => i.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(i => new
            {
                i.RecebimentoCompraId,
                i.CompraItemId
            })
            .IsUnique();

            builder.HasIndex(i => i.CompraItemId);
            builder.HasIndex(i => i.ProdutoId);
        }
    }
}
