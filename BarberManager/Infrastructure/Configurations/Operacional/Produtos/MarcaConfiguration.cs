using Domain.Entities.Produtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Operacional.Produtos
{
    public class MarcaConfiguration
        : IEntityTypeConfiguration<Marca>
    {
        public void Configure(EntityTypeBuilder<Marca> builder)
        {
            builder.ToTable("Marcas");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.EmpresaId)
                .IsRequired();

            builder.Property(m => m.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(m => m.Descricao)
                .HasMaxLength(500);

            builder.Property(m => m.Ativo)
                .IsRequired();

            builder.HasIndex(m => new { m.EmpresaId, m.Nome })
                .IsUnique()
                .HasDatabaseName("IX_Marcas_EmpresaId_Nome");

            builder.HasMany(m => m.Produtos)
                .WithOne(p => p.Marca)
                .HasForeignKey(p => p.MarcaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
