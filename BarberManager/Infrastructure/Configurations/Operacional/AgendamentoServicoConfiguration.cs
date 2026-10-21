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
    public sealed class AgendamentoServicoConfiguration
     : IEntityTypeConfiguration<AgendamentoServico>
    {
        public void Configure(EntityTypeBuilder<AgendamentoServico> builder)
        {
            builder.ToTable("AgendamentoServicos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Valor)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.DuracaoMinutos)
                .IsRequired();

            builder.HasIndex(x => x.AgendamentoId);

            builder.HasIndex(x => x.ServicoId);
        }
    }
}
