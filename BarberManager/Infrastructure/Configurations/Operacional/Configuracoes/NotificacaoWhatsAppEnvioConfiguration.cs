using Domain.Entities.Common;
using Domain.Entities.Notificacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Operacional.Notificacoes;

public sealed class NotificacaoWhatsAppEnvioConfiguration
    : IEntityTypeConfiguration<NotificacaoWhatsAppEnvio>
{
    public void Configure(
        EntityTypeBuilder<NotificacaoWhatsAppEnvio> builder)
    {
        builder.ToTable("NotificacoesWhatsAppEnvios");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.EmpresaId)
            .IsRequired();

        builder.Property(x => x.OutboxEventId)
            .IsRequired();

        builder.Property(x => x.HashConteudo)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Tentativas)
            .IsRequired();

        builder.Property(x => x.ProviderMessageId)
            .HasMaxLength(200);

        builder.Property(x => x.UltimoErro)
            .HasMaxLength(2000);

        // Impede mais de um envio lógico para o mesmo evento/empresa.
        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.OutboxEventId
        })
        .IsUnique();

        // Apoia consultas de processamento e recuperação de leases.
        builder.HasIndex(x => new
        {
            x.Status,
            x.LeaseExpiraEmUtc
        });

        // O registro de envio deve pertencer a um evento existente.
        builder.HasOne<OutboxOperacionalMessage>()
            .WithMany()
            .HasForeignKey(x => x.OutboxEventId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}