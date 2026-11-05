
using Domain.Entities.Financeiro;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Operacional
{
    public class ConfiguracaoAssasClientConfiguration
        : IEntityTypeConfiguration<ConfiguracaoAssasClient>
    {
        public void Configure(
            EntityTypeBuilder<ConfiguracaoAssasClient> builder)
        {
            builder.ToTable("ConfigurarcaoAssasCliente");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmpresaId)
                .IsRequired();

            builder.Property(x => x.ApiTokenProtegido)
                .IsRequired();

            builder.Property(x => x.WebhooksTokenProtegido)
                .IsRequired();

            builder.Property(x => x.Ambiente)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.Ativo)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(x => x.DataUltimaValidacao)
                .IsRequired(false);

            builder.HasIndex(x => x.EmpresaId)
                .IsUnique()
                .HasDatabaseName(
                    "UX_ConfigurarcaoAssasCliente_EmpresaId");
        }
    }
}
