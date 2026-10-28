using Domain.Entities.Vendas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Operacional.Vendas
{
    public class VendaConfiguration : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.ToTable("Vendas");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.EmpresaId)
                .IsRequired();

            builder.Property(v => v.AgendamentoId)
                .IsRequired(false);

            builder.Property(v => v.ClienteId)
                .IsRequired(false);

            builder.Property(v => v.ProfissionalId)
                .IsRequired(false);

            builder.Property(v => v.UsuarioResponsavelId)
                .IsRequired();

            builder.Property(v => v.DataAbertura)
                .IsRequired();

            builder.Property(v => v.DataFinalizacao)
                .IsRequired(false);

            builder.Property(v => v.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(v => v.Subtotal)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(v => v.Desconto)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(v => v.ValorTotal)
                .HasPrecision(18, 2)
                .IsRequired();

            // Um agendamento pode originar no máximo uma venda.
            // Vendas manuais podem ter AgendamentoId nulo.
            builder.HasOne(v => v.Agendamento)
                .WithOne(a => a.Venda)
                .HasForeignKey<Venda>(v => v.AgendamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índices para consultas por empresa.
            builder.HasIndex(v => v.EmpresaId);

            builder.HasIndex(v => new
            {
                v.EmpresaId,
                v.Status
            });

            builder.HasIndex(v => new
            {
                v.EmpresaId,
                v.DataAbertura
            });

            // Impede duplicidade de venda para o mesmo agendamento.
            builder.HasIndex(v => v.AgendamentoId)
                .IsUnique()
                .HasFilter("[AgendamentoId] IS NOT NULL");
        }
    }
}
