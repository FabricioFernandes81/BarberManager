using Domain.Entities.Vendas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Operacional.Vendas
{
    public class VendaItemConfiguration
        : IEntityTypeConfiguration<VendaItem>
    {
        public void Configure(EntityTypeBuilder<VendaItem> builder)
        {
            builder.ToTable("VendaItens");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.VendaId)
                .IsRequired();

            builder.Property(i => i.TipoItem)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(i => i.ItemId)
                .IsRequired();

            builder.Property(i => i.Descricao)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(i => i.Quantidade)
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

            builder.HasOne(i => i.Venda)
                .WithMany(v => v.Itens)
                .HasForeignKey(i => i.VendaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(i => i.VendaId);

            builder.HasIndex(i => new
            {
                i.TipoItem,
                i.ItemId
            });
        }
    }
}
