using Domain.Entities.Estoque;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Operacional.Estoque
{
    public class EstoqueProdutoConfiguration
       : IEntityTypeConfiguration<EstoqueProduto>
    {
        public void Configure(EntityTypeBuilder<EstoqueProduto> builder)
        {
            builder.ToTable("EstoquesProdutos");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.EmpresaId)
                .IsRequired();

            builder.Property(e => e.ProdutoId)
                .IsRequired();

            builder.Property(e => e.QuantidadeAtual)
                .HasPrecision(18, 3)
                .IsRequired();

            builder.Property(e => e.EstoqueMinimo)
                .HasPrecision(18, 3)
                .IsRequired();

            builder.Property(e => e.DataAtualizacaoEstoque)
                .IsRequired();

            // Garante um único registro de estoque por produto.
            builder.HasIndex(e => e.ProdutoId)
                .IsUnique()
                .HasDatabaseName("IX_EstoquesProdutos_ProdutoId");

            // Facilita consultas de estoque por empresa.
            builder.HasIndex(e => new
            {
                e.EmpresaId,
                e.QuantidadeAtual
            })
                .HasDatabaseName(
                    "IX_EstoquesProdutos_EmpresaId_QuantidadeAtual");
        }
    }
}
