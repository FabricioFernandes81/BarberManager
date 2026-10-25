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
    public sealed class ConfiguracaoAgendaEmpresaConfiguration
         : IEntityTypeConfiguration<ConfiguracaoAgendaEmpresa>
    {
        public void Configure(
            EntityTypeBuilder<ConfiguracaoAgendaEmpresa> builder)
        {
            builder.ToTable("ConfiguracoesAgendaEmpresa");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmpresaId)
                .IsRequired();

            builder.Property(x => x.ToleranciaEncerramentoMinutos)
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasIndex(x => x.EmpresaId)
                .IsUnique();

            builder.ToTable(table =>
                table.HasCheckConstraint(
                    "CK_ConfiguracoesAgendaEmpresa_Tolerancia",
                    "[ToleranciaEncerramentoMinutos] >= 0"));
        }
    }
}
