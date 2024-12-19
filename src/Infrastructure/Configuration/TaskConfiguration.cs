using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Configuration for the Task entity.
/// </summary>
public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
{
    /// <summary>
    /// Configures the entity and its relationships.
    /// </summary>
    /// <param name="builder">The builder to configure the entity.</param>
    public void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(25).HasConversion<ProjectTaskId.EfCoreValueConverter>();
        builder.Property(x => x.ProjectId).HasMaxLength(25).HasConversion<ProjectId.EfCoreValueConverter>();
        builder.Property(x => x.WorkspaceId).HasMaxLength(25).HasConversion<WorkspaceId.EfCoreValueConverter>();
        builder.Property(x => x.Title).HasMaxLength(StringSizes.Title).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(StringSizes.Max);
        builder.Property(x => x.Cost).HasColumnType("decimal(18,2)");
        builder.Property(x => x.Status).HasConversion<string>();
        builder.Property(x => x.Priority).HasConversion<string>();
        builder.Property(x => x.IsClosed).HasDefaultValue(false);
        builder.Property(x => x.PaymentStatus).HasConversion<string>();
        builder.Property(x => x.OpenTaskStatus).HasConversion<string>();
        builder.Property(x => x.Type).HasConversion<string>();
        builder.Property(x => x.CreatedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.CreatedDate).HasConversion<DateTime>();
        builder.Property(x => x.UpdatedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.UpdatedDate).HasConversion<DateTime>();

        builder.HasOne(t => t.Project).WithMany(p => p.ProjectTasks).HasForeignKey(t => t.ProjectId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(t => t.Workspace).WithMany(w => w.ProjectTasks).HasForeignKey(t => t.WorkspaceId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(t => t.User).WithMany(u => u.Tasks).HasForeignKey(t => t.CreatedBy).OnDelete(DeleteBehavior.Restrict);
    }
}
