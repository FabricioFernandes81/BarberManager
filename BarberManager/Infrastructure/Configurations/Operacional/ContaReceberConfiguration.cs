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

            builder.Property(x => x.Descricao)
                .HasMaxLength(300);

            builder.Property(x => x.Valor)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.ValorRecebido)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Cliente)
                .WithMany()
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Recebimentos)
                .WithOne(x => x.ContaReceber)
                .HasForeignKey(x => x.ContaReceberId);

            builder.HasIndex(x => x.EmpresaId);

            builder.HasIndex(x => x.Status);

            builder.HasIndex(x => x.DataVencimento);
        }
    }

}
