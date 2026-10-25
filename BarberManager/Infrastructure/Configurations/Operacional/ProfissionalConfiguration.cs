using Domain.Entities.Profissionais;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Operacional
{
    public class ProfissionalConfiguration : IEntityTypeConfiguration<Profissional>
    {
        public void Configure(EntityTypeBuilder<Profissional> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UsuarioId)
                    .IsRequired(false);


            builder.HasIndex(x => x.UsuarioId)
                .IsUnique()
                .HasFilter("[UsuarioId] IS NOT NULL");

            builder.HasIndex(x => x.EmpresaId);
            builder.HasIndex(x => x.Cpf);

            builder.Property(x => x.ToleranciaEncerramentoMinutos)
    .IsRequired(false);

            builder.ToTable(table =>
                table.HasCheckConstraint(
                    "CK_Profissionais_ToleranciaEncerramento",
                    "[ToleranciaEncerramentoMinutos] IS NULL " +
                    "OR [ToleranciaEncerramentoMinutos] >= 0"));
        }
    }
}
