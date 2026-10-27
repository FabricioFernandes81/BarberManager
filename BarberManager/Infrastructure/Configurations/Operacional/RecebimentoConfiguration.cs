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
            builder.ToTable("Recebimento", table =>
            {
                table.HasCheckConstraint(
                    "CK_Recebimento_Valor_Positivo",
                    "[Valor] > 0");
            });

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ContaReceberId)
                .IsRequired();

            builder.Property(x => x.FormaPagamentoId)
                .IsRequired();

            builder.Property(x => x.DataRecebimento)
                .IsRequired();

            builder.Property(x => x.Valor)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Observacao)
                .HasMaxLength(500);

            builder.HasOne(x => x.ContaReceber)
                .WithMany(x => x.Recebimentos)
                .HasForeignKey(x => x.ContaReceberId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.FormaPagamento)
                .WithMany()
                .HasForeignKey(x => x.FormaPagamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.ContaReceberId);

            builder.HasIndex(x => x.FormaPagamentoId);

            builder.HasIndex(x => x.DataRecebimento);
        }
    }

}
