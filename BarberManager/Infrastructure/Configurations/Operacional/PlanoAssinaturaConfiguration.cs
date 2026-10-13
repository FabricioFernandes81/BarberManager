using Domain.Entities.Assinaturas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Operacional
{
    public class PlanoAssinaturaConfiguration
     : IEntityTypeConfiguration<PlanoAssinatura>
    {
        public void Configure(EntityTypeBuilder<PlanoAssinatura> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.Nome
            });

            builder.HasMany(x => x.Servicos)
                .WithOne(x => x.PlanoAssinatura)
                .HasForeignKey(x => x.PlanoAssinaturaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Beneficios)
                .WithOne(x => x.PlanoAssinatura)
                .HasForeignKey(x => x.PlanoAssinaturaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Assinaturas)
                .WithOne(x => x.PlanoAssinatura)
                .HasForeignKey(x => x.PlanoAssinaturaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
