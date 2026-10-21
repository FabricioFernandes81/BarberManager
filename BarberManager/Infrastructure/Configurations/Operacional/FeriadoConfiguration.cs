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
    public sealed class FeriadoConfiguration
    : IEntityTypeConfiguration<Feriado>
    {
        public void Configure(EntityTypeBuilder<Feriado> builder)
        {
            builder.ToTable("Feriados");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmpresaId)
                .IsRequired();

            builder.Property(x => x.Data)
                .IsRequired();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.Data
            });
            builder.Property(x => x.Ativo)
    .IsRequired();

            builder.Property(x => x.Data)
                .HasColumnType("date")
                .IsRequired();

            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.Data
            });
        }
    }
}
