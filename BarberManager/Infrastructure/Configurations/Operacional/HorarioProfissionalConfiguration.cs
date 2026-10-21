using Domain.Entities.Agendamentos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Operacional
{
    public sealed class HorarioProfissionalConfiguration
     : IEntityTypeConfiguration<HorarioProfissional>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<HorarioProfissional> builder)
        {
            builder.ToTable("HorariosProfissionais");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProfissionalId)
                .IsRequired();

            builder.Property(x => x.DiaSemana)
                .IsRequired();

            builder.Property(x => x.HoraInicio)
                .IsRequired();

            builder.Property(x => x.HoraFim)
                .IsRequired();

            builder.HasIndex(x => new
            {
                x.ProfissionalId,
                x.DiaSemana
            });
            builder.Property(x => x.Ativo)
                 .IsRequired();
            builder.Property(x => x.EmpresaId)
                 .IsRequired();


            builder.HasOne(x => x.Profissional)
                .WithMany()
                .HasForeignKey(x => x.ProfissionalId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
