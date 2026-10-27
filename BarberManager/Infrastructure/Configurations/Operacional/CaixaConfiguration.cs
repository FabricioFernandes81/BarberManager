using Domain.Entities.Financeiro;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Operacional
{
    public class CaixaConfiguration : IEntityTypeConfiguration<Caixa>
    {
        public void Configure(EntityTypeBuilder<Caixa> builder)
        {
            builder.ToTable("Caixa");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SaldoInicial)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.SaldoFinal)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Observacao)
                .HasMaxLength(1000);

            builder.HasMany(x => x.Movimentos)
                .WithOne(x => x.Caixa)
                .HasForeignKey(x => x.CaixaId);

            builder.HasIndex(x => x.EmpresaId);

            //    builder.HasIndex(x => x.FilialId);

            builder.HasIndex(x => x.UsuarioId);

            builder.HasIndex(x => x.Status);

            builder.HasIndex(x => x.DataAbertura);

            builder.Property(x => x.SaldoEsperadoFechamento)
    .HasColumnType("decimal(18,2)");

            builder.Property(x => x.DiferencaFechamento)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.ObservacaoFechamento)
                .HasMaxLength(1000);

             builder.HasIndex(x => new
             {
                 x.EmpresaId,
                 x.UsuarioId
             })
                 .IsUnique()
                 .HasFilter("[Status] = 1");

        }
    }
}
