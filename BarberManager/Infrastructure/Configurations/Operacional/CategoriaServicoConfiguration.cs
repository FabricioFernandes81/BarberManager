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
    public class CategoriaServicoConfiguration : IEntityTypeConfiguration<CategoriaServico>
    {
        public void Configure(EntityTypeBuilder<CategoriaServico> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.EmpresaId, x.Nome });


        }
    }
}
