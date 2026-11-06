
using Domain.Entities.Financeiro;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Operacional
{
    public class ClienteAsaasOperacionalConfiguration
        : IEntityTypeConfiguration<ClienteAsaasOperacional>
    {
        public void Configure(
            EntityTypeBuilder<ClienteAsaasOperacional> builder)
        {
            builder.ToTable("ClientesAsaasOperacionais");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmpresaId)
                .IsRequired();

            builder.Property(x => x.ClienteId)
                .IsRequired();

            builder.Property(x => x.ClienteAsaasId)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.DataCriacaoGatewayUtc)
                .IsRequired();

            builder.Property(x => x.DataAtualizacaoGatewayUtc)
                .IsRequired(false);

            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.ClienteId
            })
            .IsUnique()
            .HasDatabaseName(
                "UX_ClientesAsaasOperacionais_EmpresaId_ClienteId");

            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.ClienteAsaasId
            })
            .IsUnique()
            .HasDatabaseName(
                "UX_ClientesAsaasOperacionais_EmpresaId_ClienteAsaasId");
        }
    }
}
