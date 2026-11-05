using Domain.Entities.Financeiro;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Operacional
{
    public class PagamentoConfiguration : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.ToTable("Pagamento");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmpresaId)
                .IsRequired();

            builder.Property(x => x.ChaveIdempotencia)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Valor)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Observacao)
                .HasMaxLength(500);

            builder.HasOne(x => x.ContaPagar)
                .WithMany(x => x.Pagamentos)
                .HasForeignKey(x => x.ContaPagarId);

            builder.HasOne(x => x.FormaPagamento)
                .WithMany()
                .HasForeignKey(x => x.FormaPagamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.DataPagamento);

            builder.Property(x => x.ValorPagoAposPagamento)
    .HasPrecision(18, 2)
    .IsRequired();

            builder.Property(x => x.SaldoRestanteAposPagamento)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.StatusContaAposPagamento)
                .HasConversion<int>()
                .IsRequired();
            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.ChaveIdempotencia
            })
                .IsUnique()
                .HasDatabaseName(
                    "UX_Pagamento_EmpresaId_ChaveIdempotencia");
        }
    }
}