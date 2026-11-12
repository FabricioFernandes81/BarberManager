using Domain.Entities.Common.Enuns;
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
    public class FornecedorConfiguration
        : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("Fornecedores");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.EmpresaId)
                .IsRequired();

            builder.Property(f => f.Nome)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(f => f.NomeFantasia)
                .HasMaxLength(150);

            builder.Property(f => f.Documento)
                .HasMaxLength(14);

            builder.Property(f => f.Email)
                .HasMaxLength(254);

            builder.Property(f => f.Telefone)
                .HasMaxLength(20);

            builder.Property(f => f.Observacao)
                .HasMaxLength(1000);

            builder.Property(f => f.Ativo)
                .IsRequired();

            // Índice para consultas de fornecedores por empresa.
            builder.HasIndex(f => new
            {
                f.EmpresaId,
                f.Nome
            })
                .HasDatabaseName("IX_Fornecedores_EmpresaId_Nome");

            // Documento único por empresa, quando informado.
            builder.HasIndex(f => new
            {
                f.EmpresaId,
                f.Documento
            })
                .IsUnique()
                .HasFilter("[Documento] IS NOT NULL")
                .HasDatabaseName("IX_Fornecedores_EmpresaId_Documento");

            builder.Property(f => f.PoliticaFinanceiraDevolucao)
                    .HasConversion<int>()
                    .HasDefaultValue(
                        PoliticaFinanceiraDevolucao.CreditoFornecedor)
                    .IsRequired();
        }
    }
}
