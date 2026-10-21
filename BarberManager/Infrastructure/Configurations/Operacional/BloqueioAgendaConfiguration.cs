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
    public sealed class BloqueioAgendaConfiguration
    : IEntityTypeConfiguration<BloqueioAgenda>
    {
        public void Configure(EntityTypeBuilder<BloqueioAgenda> builder)
        {
            builder.ToTable("BloqueiosAgenda");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmpresaId)
                .IsRequired();

            builder.Property(x => x.DataHoraInicio)
                .IsRequired();

            builder.Property(x => x.DataHoraFim)
                .IsRequired();

            builder.Property(x => x.Motivo)
                .HasMaxLength(500);

            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.DataHoraInicio
            });
        }
    }
}
