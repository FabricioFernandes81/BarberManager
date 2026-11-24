
using Domain.Entities.Notificacoes;
using Domain.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Operacional.Notificacoes;

public sealed class InteracaoWhatsAppPendenteConfiguration
    : IEntityTypeConfiguration<InteracaoWhatsAppPendente>
{
    public void Configure(
        EntityTypeBuilder<InteracaoWhatsAppPendente> builder)
    {
        builder.ToTable("InteracoesWhatsAppPendentes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.EmpresaId)
            .IsRequired();

        builder.Property(x => x.ClienteId)
            .IsRequired();

        builder.Property(x => x.InstanceName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Telefone)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.TipoInteracao)
            .HasConversion<string>()
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(x => x.AgendamentoSelecionadoId);

        builder.Property(x => x.MessageIdOrigem)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.CriadaEmUtc)
            .IsRequired();

        builder.Property(x => x.ExpiraEmUtc)
            .IsRequired();

        builder.Property(x => x.ConcluidaEmUtc);

        builder.HasMany(x => x.Opcoes)
            .WithOne(x => x.InteracaoWhatsAppPendente)
            .HasForeignKey(x => x.InteracaoWhatsAppPendenteId)
            .OnDelete(DeleteBehavior.Cascade);

        // Idempotência da mensagem que iniciou a interação.
        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.MessageIdOrigem
        })
        .IsUnique();

        // Uma interação ativa por empresa, instância e telefone.
        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.InstanceName,
            x.Telefone
        })
        .IsUnique()
        .HasFilter(
            "[Status] IN ('AguardandoSelecao', 'AguardandoConfirmacao')");

        // Consulta de interações pendentes por vencimento.
        builder.HasIndex(x => new
        {
            x.Status,
            x.ExpiraEmUtc
        });

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.ClienteId,
            x.CriadaEmUtc
        });
    }
}
