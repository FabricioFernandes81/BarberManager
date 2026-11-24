
using Domain.Entities.Notificacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Operacional.Notificacoes;

public sealed class OpcaoInteracaoWhatsAppConfiguration
    : IEntityTypeConfiguration<OpcaoInteracaoWhatsApp>
{
    public void Configure(
        EntityTypeBuilder<OpcaoInteracaoWhatsApp> builder)
    {
        builder.ToTable("OpcoesInteracaoWhatsApp");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.InteracaoWhatsAppPendenteId)
            .IsRequired();

        builder.Property(x => x.AgendamentoId)
            .IsRequired();

        builder.Property(x => x.Ordem)
            .IsRequired();

        builder.Property(x => x.DataHoraInicioSnapshot)
            .IsRequired();

        builder.Property(x => x.ServicosSnapshot)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.ProfissionalSnapshot)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasOne(x => x.InteracaoWhatsAppPendente)
            .WithMany(x => x.Opcoes)
            .HasForeignKey(x => x.InteracaoWhatsAppPendenteId)
            .OnDelete(DeleteBehavior.Cascade);

        // Não permitir números repetidos na mesma interação.
        builder.HasIndex(x => new
        {
            x.InteracaoWhatsAppPendenteId,
            x.Ordem
        })
        .IsUnique();

        // Não apresentar o mesmo agendamento duas vezes.
        builder.HasIndex(x => new
        {
            x.InteracaoWhatsAppPendenteId,
            x.AgendamentoId
        })
        .IsUnique();

        // Facilita a localização de opções pelo agendamento.
        builder.HasIndex(x => x.AgendamentoId);
    }
}
