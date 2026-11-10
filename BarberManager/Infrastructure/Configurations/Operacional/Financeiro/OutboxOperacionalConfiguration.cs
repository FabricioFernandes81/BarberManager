
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Financeiro;

public sealed class OutboxOperacionalConfiguration
    : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.EventType)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.TargetWorkflow)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Payload)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Attempts)
            .IsRequired();

        builder.Property(x => x.OccurredAtUtc)
            .IsRequired();

        builder.Property(x => x.NextAttemptAtUtc)
            .IsRequired();

        builder.Property(x => x.LockedUntilUtc);

        builder.Property(x => x.ProcessedAtUtc);

        builder.Property(x => x.LastError)
            .HasMaxLength(2000);

        builder.HasIndex(x => new
        {
            x.Status,
            x.NextAttemptAtUtc,
            x.OccurredAtUtc
        });

        builder.HasIndex(x => new
        {
            x.TargetWorkflow,
            x.Status
        });
    }
}
