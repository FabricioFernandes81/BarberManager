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
    public class CompraConfiguration
       : IEntityTypeConfiguration<Compra>
    {
        public void Configure(EntityTypeBuilder<Compra> builder)
        {
            builder.ToTable("Compras");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.EmpresaId)
                .IsRequired();

            builder.Property(c => c.FornecedorId)
                .IsRequired();

            builder.Property(c => c.NumeroDocumento)
                .HasMaxLength(50);

            builder.Property(c => c.SerieDocumento)
                .HasMaxLength(20);

            builder.Property(c => c.DataEmissao)
                .IsRequired();

            builder.Property(c => c.DataCompra)
                .IsRequired();

            builder.Property(c => c.Subtotal)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(c => c.Desconto)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(c => c.Frete)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(c => c.OutrasDespesas)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(c => c.ValorTotal)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(c => c.Observacao)
                .HasMaxLength(1000);

            builder.Property(c => c.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.HasOne(c => c.Fornecedor)
                .WithMany()
                .HasForeignKey(c => c.FornecedorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Itens)
                .WithOne(i => i.Compra)
                .HasForeignKey(i => i.CompraId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => c.EmpresaId);

            builder.Property(c => c.RowVersion)
    .IsRowVersion();


            builder.HasIndex(c => new
            {
                c.EmpresaId,
                c.FornecedorId,
                c.DataCompra
            });

            builder.HasIndex(c => new
            {
                c.EmpresaId,
                c.Status
            });

          
        }
    }
}
