using Domain.Entities.Financeiro;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Operacional
{
    public class EventoWebhookAsaasConfiguration
        : IEntityTypeConfiguration<EventoWebhookAsaas>
    {
        public void Configure(
            EntityTypeBuilder<EventoWebhookAsaas> builder)
        {
            builder.ToTable("EventosWebhookAsaas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmpresaId)
                .IsRequired();

            builder.Property(x => x.EventoId)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.PagamentoGatewayId)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.TipoEvento)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Payload)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.Tentativas)
                .IsRequired();

            builder.Property(x => x.RecebidoEmUtc)
                .IsRequired();

            builder.Property(x => x.ProximaTentativaEmUtc)
                .IsRequired();

            builder.Property(x => x.UltimoErro)
                .HasMaxLength(2000);

            // Impede que o mesmo evento seja registrado
            // mais de uma vez para a mesma empresa.
            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.EventoId
            })
            .IsUnique()
            .HasDatabaseName(
                "UX_EventosWebhookAsaas_Empresa_Evento");

            // Índice para busca dos eventos pendentes.
            builder.HasIndex(x => new
            {
                x.Status,
                x.ProximaTentativaEmUtc,
                x.BloqueadoAteUtc
            })
            .HasDatabaseName(
                "IX_EventosWebhookAsaas_Processamento");

            // Índice para localizar eventos de uma cobrança.
            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.PagamentoGatewayId
            });
        }
    }
}