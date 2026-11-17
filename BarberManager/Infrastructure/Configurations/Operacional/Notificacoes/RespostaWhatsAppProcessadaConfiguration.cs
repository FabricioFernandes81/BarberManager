
using Domain.Entities.Notificacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Operacional.Notificacoes;

public sealed class RespostaWhatsAppProcessadaConfiguration
    : IEntityTypeConfiguration<RespostaWhatsAppProcessada>
{
    public void Configure(
        EntityTypeBuilder<RespostaWhatsAppProcessada> builder)
    {
        builder.ToTable("RespostasWhatsAppProcessadas");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.EmpresaId)
            .IsRequired();

        builder.Property(x => x.InstanceName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.ProviderMessageId)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Telefone)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Comando)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.Resultado)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.DataProcessamentoUtc)
            .IsRequired();

        // Uma mensagem do provedor só pode ser processada
        // uma vez dentro da mesma empresa.
        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.ProviderMessageId
        })
        .IsUnique();

        builder.HasIndex(x => new
        {
            x.EmpresaId,
            x.AgendamentoId
        });
    }
}
