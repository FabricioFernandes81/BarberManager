using Domain.Entities.Saas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Saas
{
    public class TransacaoPagamentoConfigurations
         : IEntityTypeConfiguration<TransacaoPagamento>
    {
        public void Configure(
            EntityTypeBuilder<TransacaoPagamento> builder)
        {
            builder.ToTable("TransacoesPagamento");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Provedor)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.ReferenciaGateway)
                .HasMaxLength(200);

            builder.Property(x => x.ChaveIdempotencia)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Valor)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.CodigoErro)
                .HasMaxLength(100);

            builder.Property(x => x.MensagemErro)
                .HasMaxLength(1000);

            // Impede reutilizar a mesma chave para o mesmo provedor.
            builder.HasIndex(x => new
            {
                x.Provedor,
                x.ChaveIdempotencia
            })
                .IsUnique();

            // Uma referência externa pode existir apenas uma vez
            // dentro do mesmo provedor, quando estiver preenchida.
            builder.HasIndex(x => new
            {
                x.Provedor,
                x.ReferenciaGateway
            })
                .IsUnique()
                .HasFilter("[ReferenciaGateway] IS NOT NULL");

            // Facilita a consulta do histórico de uma fatura.
            builder.HasIndex(x => new
            {
                x.FaturaId,
                x.DataCadastro
            });

            builder.HasOne(x => x.Fatura)
                .WithMany(x => x.TransacoesPagamento)
                .HasForeignKey(x => x.FaturaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
