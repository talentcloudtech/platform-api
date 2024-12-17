using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Configuration for the TaskAssignment entity.
/// </summary>
public class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
{
    /// <summary>
    /// Configures the entity and its relationships.
    /// </summary>
    /// <param name="builder">The builder to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Proposal> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(25).HasConversion<ProposalId.EfCoreValueConverter>();
        builder.Property(x => x.TaskId).HasMaxLength(25).HasConversion<ProjectTaskId.EfCoreValueConverter>();
        builder.Property(x => x.UserId).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.StartDate).HasConversion<DateTime>();
        builder.Property(x => x.EndDate).HasConversion<DateTime>();
        builder.Property(x => x.CostEstimation).HasConversion<decimal>();
        builder.Property(x => x.Status).HasConversion<string>();
        builder.Property(x => x.Description).HasMaxLength(StringSizes.Max);

        builder.Property(x => x.CreatedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.CreatedDate).HasConversion<DateTime>();
        builder.Property(x => x.UpdatedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.UpdatedDate).HasConversion<DateTime>();

        builder.HasOne(ta => ta.ProjectTask).WithMany(t => t.Proposals).HasForeignKey(ta => ta.TaskId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(ta => ta.User).WithMany(u => u.Proposals).HasForeignKey(ta => ta.UserId).OnDelete(DeleteBehavior.Restrict);
    }
}
