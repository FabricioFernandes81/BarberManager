using Domain.Entities.Assinaturas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Operacional
{
    public class BeneficioPlanoConfiguration
     : IEntityTypeConfiguration<BeneficioPlano>
    {
        public void Configure(EntityTypeBuilder<BeneficioPlano> builder)
        {
            builder.ToTable("BeneficiosPlano");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmpresaId)
                .IsRequired();

            builder.Property(x => x.PlanoAssinaturaId)
                .IsRequired();

            builder.Property(x => x.Nome)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasMaxLength(500);

            builder.Property(x => x.Tipo)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.Valor)
                .HasPrecision(18, 2);

            builder.Property(x => x.Ativo)
                .HasDefaultValue(true)
                .IsRequired();

            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.PlanoAssinaturaId
            });

            builder.HasOne(x => x.PlanoAssinatura)
                .WithMany(x => x.Beneficios)
                .HasForeignKey(x => x.PlanoAssinaturaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
