using Domain.Entities.Servicos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Operacional
{
    public class ProfissionalServicoConfiguration : IEntityTypeConfiguration<ProfissionalServicos>
    {
        public void Configure(EntityTypeBuilder<ProfissionalServicos> builder)
        {
            builder.HasKey(x => x.Id);


            builder.HasIndex(x => new
            {
                x.ProfissionalId,
                x.ServicoId
            }).IsUnique();

            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.ProfissionalId,
                x.Ativo
            });

            builder.HasOne(x => x.Profissional)
                .WithMany(x => x.Servicos)
                .HasForeignKey(x => x.ProfissionalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Servico)
                .WithMany(x => x.Profissionais)
                .HasForeignKey(x => x.ServicoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
