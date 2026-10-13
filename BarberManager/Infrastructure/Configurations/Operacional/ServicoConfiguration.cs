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
    public class ServicoConfiguration : IEntityTypeConfiguration<Servico>
    {
        public void Configure(EntityTypeBuilder<Servico> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.EmpresaId, x.Nome });

            builder.HasOne(x => x.CategoriaServico)
                .WithMany(x => x.Servicos)
                .HasForeignKey(x => x.CategoriaServicoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
