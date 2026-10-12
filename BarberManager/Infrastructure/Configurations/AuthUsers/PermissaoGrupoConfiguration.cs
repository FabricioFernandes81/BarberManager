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
    public class PermissaoGrupoConfiguration : IEntityTypeConfiguration<PermissaoGrupo>
    {
        public void Configure(EntityTypeBuilder<PermissaoGrupo> builder)
        {
            builder.HasKey(x => x.Id);

            /*  builder.HasMany(x=>x.Permissoes)
                  .WithOne(x=>x.PermissaoGrupo)
                  .HasForeignKey(x=>x.PermissaoGrupoId)
                  .OnDelete(DeleteBehavior.Restrict);*/

            builder.HasMany(x => x.GrupoPermissao)
                  .WithOne(x => x.PermissaoGrupo)
                  .HasForeignKey(x => x.PermissaoGrupoId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
