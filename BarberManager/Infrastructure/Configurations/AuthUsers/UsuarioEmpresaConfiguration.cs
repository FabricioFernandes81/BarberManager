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
    public class UsuarioEmpresaConfiguration
        : IEntityTypeConfiguration<UsuarioEmpresa>
    {
        public void Configure(EntityTypeBuilder<UsuarioEmpresa> builder)
        {
            builder.ToTable("UsuariosEmpresa");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UsuarioId)
                .IsRequired();

            builder.Property(x => x.EmpresaId)
                .IsRequired();

            builder.Property(x => x.Ativo)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(x => x.DataVinculo)
                .IsRequired();

            builder.Property(x => x.DataDesvinculo)
                .IsRequired(false);

            // Impede vínculos duplicados para o mesmo usuário
            // e a mesma empresa.
            builder.HasIndex(x => new
            {
                x.UsuarioId,
                x.EmpresaId
            })
                .IsUnique();

            // O usuário pertence ao modelo de autenticação.
            builder.HasOne(x => x.Usuario)
                .WithMany()
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        
        }
    }
}
