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
    public class PagamentoConfiguration : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.ToTable("Pagamento");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Valor)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Observacao)
                .HasMaxLength(500);

            builder.HasOne(x => x.ContaPagar)
                .WithMany(x => x.Pagamentos)
                .HasForeignKey(x => x.ContaPagarId);

            builder.HasOne(x => x.FormaPagamento)
                .WithMany()
                .HasForeignKey(x => x.FormaPagamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.DataPagamento);
        }
    }

}
