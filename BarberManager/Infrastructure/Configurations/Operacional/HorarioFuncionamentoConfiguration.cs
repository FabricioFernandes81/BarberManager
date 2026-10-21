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
    public sealed class HorarioFuncionamentoConfiguration
    : IEntityTypeConfiguration<HorarioFuncionamento>
    {
        public void Configure(EntityTypeBuilder<HorarioFuncionamento> builder)
        {
            builder.ToTable("HorariosFuncionamento");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmpresaId)
                .IsRequired();

            builder.Property(x => x.DiaSemana)
                .IsRequired();

            builder.Property(x => x.HoraInicio)
                .IsRequired();

            builder.Property(x => x.HoraFim)
                .IsRequired();
            builder.Property(x => x.Ativo)
                .IsRequired();

            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.DiaSemana
            });

        }
    }
}
