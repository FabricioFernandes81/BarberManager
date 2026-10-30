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
    public class ProdutoConfiguration
        : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.EmpresaId)
                .IsRequired();

            builder.Property(p => p.Nome)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(p => p.Descricao)
                .HasMaxLength(1000);

            builder.Property(p => p.CodigoInterno)
                .HasMaxLength(50);

            builder.Property(p => p.CodigoBarras)
                .HasMaxLength(50);

            builder.Property(p => p.ValorVenda)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(p => p.Custo)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(p => p.ControlaEstoque)
                .IsRequired();

            builder.Property(p => p.Ativo)
                .IsRequired();

            // Índice para consultas de produtos por empresa.
            builder.HasIndex(p => new
            {
                p.EmpresaId,
                p.Nome
            })
                .HasDatabaseName("IX_Produtos_EmpresaId_Nome");

            // Código interno único dentro da empresa,
            // quando informado.
            builder.HasIndex(p => new
            {
                p.EmpresaId,
                p.CodigoInterno
            })
                .IsUnique()
                .HasFilter("[CodigoInterno] IS NOT NULL")
                .HasDatabaseName("IX_Produtos_EmpresaId_CodigoInterno");

            // Código de barras único dentro da empresa,
            // quando informado.
            builder.HasIndex(p => new
            {
                p.EmpresaId,
                p.CodigoBarras
            })
                .IsUnique()
                .HasFilter("[CodigoBarras] IS NOT NULL")
                .HasDatabaseName("IX_Produtos_EmpresaId_CodigoBarras");

            // Relacionamento com categoria.
            builder.HasOne(p => p.CategoriaProduto)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com marca.
            builder.HasOne(p => p.Marca)
                .WithMany(m => m.Produtos)
                .HasForeignKey(p => p.MarcaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Um produto pode possuir um registro de estoque.
            builder.HasOne(p => p.Estoque)
                .WithOne()
                .HasForeignKey<Domain.Entities.Estoque.EstoqueProduto>(
                    e => e.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Histórico de movimentações do produto.
            builder.HasMany(p => p.MovimentacoesEstoque)
                .WithOne()
                .HasForeignKey(m => m.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
