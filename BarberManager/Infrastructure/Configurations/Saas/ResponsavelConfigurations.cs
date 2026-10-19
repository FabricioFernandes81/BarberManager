using Domain.Entities.Saas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Saas
{
    public class ResponsavelConfigurations
    : IEntityTypeConfiguration<Responsavel>
    {
        public void Configure(EntityTypeBuilder<Responsavel> builder)
        {
            builder.ToTable("Responsaveis");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Telefone)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(x => x.UsuarioId)
                .IsUnique();
        }
    }
}
