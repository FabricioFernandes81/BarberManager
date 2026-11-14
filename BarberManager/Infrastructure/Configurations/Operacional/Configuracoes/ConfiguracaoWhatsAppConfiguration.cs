
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ConfiguracaoWhatsAppConfiguration
    : IEntityTypeConfiguration<ConfiguracaoWhatsApp>
{
    public void Configure(EntityTypeBuilder<ConfiguracaoWhatsApp> builder)
    {
        builder.ToTable("ConfiguracoesWhatsApp");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.InstanceName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.NumeroWhatsApp)
            .HasMaxLength(20);

        builder.Property(x => x.Ativo)
            .IsRequired();

        builder.HasIndex(x => x.EmpresaId)
            .IsUnique();

        builder.HasIndex(x => x.InstanceName)
            .IsUnique();
    }
}