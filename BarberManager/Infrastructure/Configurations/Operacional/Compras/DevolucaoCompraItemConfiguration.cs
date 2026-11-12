
using Domain.Entities.Compras;
using Domain.Entities.Produtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Operacional.Compras
{
    public class DevolucaoCompraItemConfiguration
        : IEntityTypeConfiguration<DevolucaoCompraItem>
    {
        public void Configure(
            EntityTypeBuilder<DevolucaoCompraItem> builder)
        {
            builder.ToTable("DevolucoesComprasItens");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.DevolucaoCompraId)
                .IsRequired();

            builder.Property(i => i.RecebimentoCompraItemId)
                .IsRequired();

            builder.Property(i => i.ProdutoId)
                .IsRequired();

            builder.Property(i => i.QuantidadeDevolvida)
                .HasPrecision(18, 3)
                .IsRequired();

            builder.HasOne(i => i.DevolucaoCompra)
                .WithMany(d => d.Itens)
                .HasForeignKey(i => i.DevolucaoCompraId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.RecebimentoCompraItem)
                .WithMany()
                .HasForeignKey(i => i.RecebimentoCompraItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Produto>()
                .WithMany()
                .HasForeignKey(i => i.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(i => i.DevolucaoCompraId);

            builder.HasIndex(i => i.RecebimentoCompraItemId);

            builder.HasIndex(i => i.ProdutoId);
        }
    }
}
