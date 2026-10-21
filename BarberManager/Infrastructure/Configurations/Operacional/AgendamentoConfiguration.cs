using Domain.Entities.Agendamentos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Operacional
{
    public class AgendamentoConfiguration : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder.ToTable("Agendamentos");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .ValueGeneratedNever();

            builder.Property(a => a.EmpresaId)
                .IsRequired();

            builder.Property(a => a.ClienteId)
                .IsRequired();

            builder.Property(a => a.ProfissionalId)
                .IsRequired();

            builder.Property(a => a.DataHoraInicio)
                .IsRequired();

            builder.Property(a => a.DataHoraFim)
                .IsRequired();

            builder.Property(a => a.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(a => a.Observacoes)
                .HasMaxLength(1000);

            builder.Property(a => a.ValorTotal)
                .HasPrecision(18, 2);

            // Índices para consultas frequentes da agenda.
            builder.HasIndex(a => new
            {
                a.EmpresaId,
                a.DataHoraInicio
            });

            builder.HasIndex(a => new
            {
                a.EmpresaId,
                a.ProfissionalId,
                a.DataHoraInicio
            });

        }
    }
}
