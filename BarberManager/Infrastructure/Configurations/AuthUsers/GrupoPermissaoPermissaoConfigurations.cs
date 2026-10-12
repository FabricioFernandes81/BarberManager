using Domain.Entities.AuthUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.AuthUsers
{
    public class GrupoPermissaoPermissaoConfigurations : IEntityTypeConfiguration<GrupoPermissaoPermissao>
    {
        public void Configure(EntityTypeBuilder<GrupoPermissaoPermissao> builder)
        {
            builder.HasKey(x => new { x.PermissaoGrupoId, x.PermissaoId });

            /* builder.HasOne(x => x.PermissaoGrupo)
                 .WithMany(x=>x.PermisaoGrupsPermissao)
              */



        }
    }
}
