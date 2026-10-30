using Domain.Entities.Produtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Operacional.Produtos
{
    public class CategoriaProdutoConfiguration
        : IEntityTypeConfiguration<CategoriaProduto>
    {
        public void Configure(EntityTypeBuilder<CategoriaProduto> builder)
        {
            builder.ToTable("CategoriasProdutos");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.EmpresaId)
                .IsRequired();

            builder.Property(c => c.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Descricao)
                .HasMaxLength(500);

            builder.Property(c => c.Ativo)
                .IsRequired();

            builder.HasIndex(c => new { c.EmpresaId, c.Nome })
                .IsUnique()
                .HasDatabaseName("IX_CategoriasProdutos_EmpresaId_Nome");

            builder.HasMany(c => c.Produtos)
                .WithOne(p => p.CategoriaProduto)
                .HasForeignKey(p => p.CategoriaProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
