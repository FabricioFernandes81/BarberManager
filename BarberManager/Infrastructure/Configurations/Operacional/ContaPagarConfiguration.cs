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
    public class ContaPagarConfiguration : IEntityTypeConfiguration<ContaPagar>
    {
        public void Configure(EntityTypeBuilder<ContaPagar> builder)
        {
            builder.ToTable("ContaPagar");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descricao)
                .HasMaxLength(300);

            builder.Property(x => x.Valor)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.ValorPago)
                .HasColumnType("decimal(18,2)");

            /*
            builder.HasOne(x => x.Fornecedor)
                .WithMany()
                .HasForeignKey(x => x.FornecedorId)
                .OnDelete(DeleteBehavior.Restrict);*/

            builder.HasMany(x => x.Pagamentos)
                .WithOne(x => x.ContaPagar)
                .HasForeignKey(x => x.ContaPagarId);

            builder.HasIndex(x => x.EmpresaId);

            builder.HasIndex(x => x.Status);

            builder.HasIndex(x => x.DataVencimento);
        }
    }

}
