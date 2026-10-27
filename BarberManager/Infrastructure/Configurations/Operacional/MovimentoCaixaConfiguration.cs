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
    public class MovimentoCaixaConfiguration : IEntityTypeConfiguration<MovimentoCaixa>
    {
        public void Configure(EntityTypeBuilder<MovimentoCaixa> builder)
        {
            builder.ToTable("MovimentoCaixa");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Valor)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Historico)
                .HasMaxLength(500);

            builder.Property(x => x.Documento)
                .HasMaxLength(100);

            builder.HasOne(x => x.Caixa)
                .WithMany(x => x.Movimentos)
                .HasForeignKey(x => x.CaixaId);

            builder.HasOne(x => x.FormaPagamento)
                .WithMany()
                .HasForeignKey(x => x.FormaPagamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.CaixaId);

            builder.HasIndex(x => x.DataMovimento);

            builder.HasIndex(x => x.TipoMovimento);
        }
    }

}
