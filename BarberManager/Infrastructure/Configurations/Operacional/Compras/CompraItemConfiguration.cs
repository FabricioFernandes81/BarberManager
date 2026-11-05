using Domain.Entities.Compras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Operacional.Compras
{
    public class CompraItemConfiguration
       : IEntityTypeConfiguration<CompraItem>
    {
        public void Configure(EntityTypeBuilder<CompraItem> builder)
        {
            builder.ToTable("CompraItens");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.CompraId)
                .IsRequired();

            builder.Property(i => i.ProdutoId)
                .IsRequired();

            builder.Property(i => i.DescricaoProduto)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(i => i.Quantidade)
                .HasPrecision(18, 3)
                .IsRequired();

            builder.Property(i => i.QuantidadeRecebida)
                .HasPrecision(18, 3)
                .IsRequired();

            builder.Property(i => i.ValorUnitario)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(i => i.Desconto)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(i => i.ValorTotal)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.HasOne(i => i.Compra)
                .WithMany(c => c.Itens)
                .HasForeignKey(i => i.CompraId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Produto)
                .WithMany()
                .HasForeignKey(i => i.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.RowVersion)
    .IsRowVersion();


            builder.HasIndex(i => i.CompraId);

            builder.HasIndex(i => i.ProdutoId);
        }
    }
}
