using Domain.Entities.Assinaturas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Operacional
{
    public class UtilizacaoAssinaturaConfiguration
       : IEntityTypeConfiguration<UtilizacaoAssinatura>
    {
        public void Configure(
            EntityTypeBuilder<UtilizacaoAssinatura> builder)
        {
            builder.ToTable("UtilizacoesAssinatura");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmpresaId)
                .IsRequired();

            builder.Property(x => x.AssinaturaClienteId)
                .IsRequired();

            builder.Property(x => x.ServicoId)
                .IsRequired();

            builder.Property(x => x.DataUtilizacao)
                .IsRequired();

            builder.Property(x => x.Quantidade)
                .HasDefaultValue(1)
                .IsRequired();

            builder.Property(x => x.Observacao)
                .HasMaxLength(500);

            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.AssinaturaClienteId,
                x.DataUtilizacao
            });

            // Uma utilização por agendamento, quando informado.
            builder.HasIndex(x => x.AgendamentoId)
                .IsUnique()
                .HasFilter("[AgendamentoId] IS NOT NULL");

            builder.HasOne(x => x.AssinaturaCliente)
                .WithMany(x => x.Utilizacoes)
                .HasForeignKey(x => x.AssinaturaClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.BeneficiarioAssinatura)
                .WithMany()
                .HasForeignKey(x => x.BeneficiarioAssinaturaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
