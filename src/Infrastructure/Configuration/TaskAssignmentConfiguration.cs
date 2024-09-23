using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Configuration for the TaskAssignment entity.
/// </summary>
public class ProjectTaskAssignmentConfiguration : IEntityTypeConfiguration<ProjectTaskAssignment>
{
    /// <summary>
    /// Configures the entity and its relationships.
    /// </summary>
    /// <param name="builder">The builder to configure the entity.</param>
    public void Configure(EntityTypeBuilder<ProjectTaskAssignment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(25).HasConversion<ProjectTaskAssignmentId.EfCoreValueConverter>();
        builder.Property(x => x.TaskId).HasMaxLength(25).HasConversion<ProjectTaskId.EfCoreValueConverter>();
        builder.Property(x => x.UserId).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.AssignedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.AssignedDate).HasConversion<DateTime>();

        builder.Property(x => x.CreatedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.CreatedDate).HasConversion<DateTime>();
        builder.Property(x => x.UpdatedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.UpdatedDate).HasConversion<DateTime>();

        builder.HasOne(ta => ta.ProjectTask).WithMany(t => t.Assignments).HasForeignKey(ta => ta.TaskId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(ta => ta.User).WithMany(u => u.TaskAssignments).HasForeignKey(ta => ta.UserId).OnDelete(DeleteBehavior.Restrict);
    }
}
