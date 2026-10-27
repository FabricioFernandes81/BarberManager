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
    public class RecebimentoConfiguration : IEntityTypeConfiguration<Recebimento>
    {
        public void Configure(EntityTypeBuilder<Recebimento> builder)
        {
            builder.ToTable("Recebimento");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Valor)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Observacao)
                .HasMaxLength(500);

            builder.HasOne(x => x.ContaReceber)
                .WithMany(x => x.Recebimentos)
                .HasForeignKey(x => x.ContaReceberId);

            builder.HasOne(x => x.FormaPagamento)
                .WithMany()
                .HasForeignKey(x => x.FormaPagamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.DataRecebimento);
        }
    }

}
