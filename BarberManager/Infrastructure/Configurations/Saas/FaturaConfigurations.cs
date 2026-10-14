using Domain.Entities.Saas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Saas
{
    public class FaturaConfigurations : IEntityTypeConfiguration<Fatura>
    {
        public void Configure(EntityTypeBuilder<Fatura> builder)
        {
           

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Valor)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Vencimento)
                .IsRequired();

            builder.Property(x => x.DataPagamento);

            builder.Property(x => x.Status)
                .HasConversion<int>()
                .IsRequired()
                .HasDefaultValue(Domain.Entities.Common.Enuns.StatusFatura.Pendente);

            builder.Property(x => x.PeriodoInicio)
                .IsRequired();

            builder.Property(x => x.PeriodoFim)
                .IsRequired();

            builder.Property(x => x.ReferenciaGateway)
                .HasMaxLength(200);

            builder.HasIndex(x => new
            {
                x.AssinaturaId,
                x.PeriodoInicio,
                x.PeriodoFim
            }).IsUnique();

            builder.HasIndex(x => x.Vencimento);

            builder.HasIndex(x => x.ReferenciaGateway)
                .IsUnique()
                .HasFilter("[ReferenciaGateway] IS NOT NULL");

            builder.HasOne(x => x.Assinatura)
                .WithMany(x => x.Faturas)
                .HasForeignKey(x => x.AssinaturaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
