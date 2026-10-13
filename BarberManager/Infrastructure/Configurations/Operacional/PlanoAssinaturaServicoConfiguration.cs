using Domain.Entities.Assinaturas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Operacional
{
    public class PlanoAssinaturaServicoConfiguration : IEntityTypeConfiguration<PlanoAssinaturaServico>
    {
        public void Configure(EntityTypeBuilder<PlanoAssinaturaServico> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new
            {
                x.PlanoAssinaturaId,
                x.ServicoId
            }).IsUnique();

            builder.HasIndex(x => new
            {
                x.EmpresaId,
                x.PlanoAssinaturaId,
                x.Ativo
            });

            builder.HasOne(x => x.PlanoAssinatura)
                .WithMany(x => x.Servicos)
                .HasForeignKey(x => x.PlanoAssinaturaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
