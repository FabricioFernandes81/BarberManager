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
    public class RolePermissaoConfiguration
            : IEntityTypeConfiguration<RolePermissao>
    {
        public void Configure(
            EntityTypeBuilder<RolePermissao> builder)
        {
            builder.HasKey(x =>
                new
                {
                    x.RoleId,
                    x.PermissaoGupoId
                });

            builder.HasOne(x => x.Role)
                .WithMany(x => x.Permissoes)
                .HasForeignKey(x => x.RoleId);

            builder.HasOne(x => x.PermissaoGrupo)
                .WithMany()
                .HasForeignKey(x => x.PermissaoGupoId);
        }
    }
}
