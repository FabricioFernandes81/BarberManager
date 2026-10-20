using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Saas
{
    internal class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("OutboxMessages", "Saas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EventType)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.TargetWorkflow)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Payload)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.LastError)
                .HasMaxLength(2000);

            builder.Property(x => x.OccurredAtUtc)
                .IsRequired();

            builder.Property(x => x.NextAttemptAtUtc)
                .IsRequired();

            builder.HasIndex(x => new
            {
                x.Status,
                x.NextAttemptAtUtc,
                x.OccurredAtUtc
            }).HasDatabaseName("IX_OutboxMessages_Pending");

            builder.HasIndex(x => new
            {
                x.TargetWorkflow,
                x.Status
            }).HasDatabaseName("IX_OutboxMessages_Target_Status");
        }
    }
}
