using Domain.Entities.Financeiro;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Operacional
{
    public class ContaReceberConfiguration : IEntityTypeConfiguration<ContaReceber>
    {
        public void Configure(EntityTypeBuilder<ContaReceber> builder)
        {
            builder.ToTable("ContaReceber");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmpresaId)
                .IsRequired();

            builder.Property(x => x.ClienteId)
                .IsRequired(false);

            builder.Property(x => x.DataEmissao)
                .IsRequired();

            builder.Property(x => x.DataVencimento)
                .IsRequired();

            builder.Property(x => x.Valor)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.ValorRecebido)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasMaxLength(300)
                .IsRequired();

            // Relacionamento opcional com Cliente.
            builder.HasOne(x => x.Cliente)
                .WithMany()
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Uma conta pode ter vários recebimentos.
            builder.HasMany(x => x.Recebimentos)
                .WithOne(x => x.ContaReceber)
                .HasForeignKey(x => x.ContaReceberId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índices para consultas por empresa.
            builder.HasIndex(x => x.EmpresaId);

            // Consultas de contas por situação.
            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.Status
            });

            // Consultas de vencimentos por empresa.
            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.DataVencimento
            });

            // Consultas financeiras por cliente.
            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.ClienteId
            });
        }
    }

}
