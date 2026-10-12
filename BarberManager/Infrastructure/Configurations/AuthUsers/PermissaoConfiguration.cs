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
    public class PermissaoConfiguration
    : IEntityTypeConfiguration<Permissao>
    {
        public void Configure(EntityTypeBuilder<Permissao> builder)
        {
            builder.HasKey(x => x.Id);

            /*builder.HasOne(x=>x.PermissaoGrupo)
                .WithMany(x=>x.Permissoes)
                .HasForeignKey(x=>x.PermissaoGrupoId)
                .OnDelete(DeleteBehavior.Restrict);
          */
            builder.HasMany(x => x.GrupoPermissao)
                .WithOne(x => x.Permissao)
                .HasForeignKey(x => x.PermissaoId)
                .OnDelete(DeleteBehavior.Restrict);







        }
    }
}
