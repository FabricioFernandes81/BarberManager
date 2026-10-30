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
    public class MovimentacaoEstoqueConfiguration
        : IEntityTypeConfiguration<MovimentacaoEstoque>
    {
        public void Configure(EntityTypeBuilder<MovimentacaoEstoque> builder)
        {
            builder.ToTable("MovimentacoesEstoque");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.EmpresaId)
                .IsRequired();

            builder.Property(m => m.ProdutoId)
                .IsRequired();

            builder.Property(m => m.Tipo)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(m => m.Origem)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(m => m.Quantidade)
                .HasPrecision(18, 3)
                .IsRequired();

            builder.Property(m => m.SaldoAnterior)
                .HasPrecision(18, 3)
                .IsRequired();

            builder.Property(m => m.SaldoPosterior)
                .HasPrecision(18, 3)
                .IsRequired();

            builder.Property(m => m.DocumentoOrigemId);

            builder.Property(m => m.UsuarioResponsavelId);

            builder.Property(m => m.DataMovimento)
                .IsRequired();

            builder.Property(m => m.Observacao)
                .HasMaxLength(500);

            // Histórico cronológico por produto.
            builder.HasIndex(m => new
            {
                m.EmpresaId,
                m.ProdutoId,
                m.DataMovimento
            })
                .HasDatabaseName(
                    "IX_MovimentacoesEstoque_Empresa_Produto_Data");

            // Pesquisa das movimentações associadas a uma origem,
            // como uma venda.
            builder.HasIndex(m => new
            {
                m.EmpresaId,
                m.Origem,
                m.DocumentoOrigemId
            })
                .HasDatabaseName(
                    "IX_MovimentacoesEstoque_Origem_Documento");

            // Relacionamento com produto.
            builder.HasOne<Domain.Entities.Produtos.Produto>()
                .WithMany(p => p.MovimentacoesEstoque)
                .HasForeignKey(m => m.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
