using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Represents the configuration for the 'Project' entity.
/// </summary>
public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    /// <summary>
    /// Configures the entity type for the Project class.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(25).HasConversion<ProjectId.EfCoreValueConverter>();
        builder.Property(x => x.WorkspaceId).HasMaxLength(25).HasConversion<WorkspaceId.EfCoreValueConverter>();
        builder.Property(x => x.Title).HasMaxLength(StringSizes.Title);
        builder.Property(x => x.Description).HasMaxLength(StringSizes.Max);
        builder.Property(x => x.StartDate).HasConversion<DateTime>();
        builder.Property(x => x.EndDate).HasConversion<DateTime>();
        builder.Property(x => x.Budget).HasColumnType("decimal(18, 2)");
        builder.Property(x => x.Status).HasConversion<string>();
        builder.Property(x => x.CreatedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.CreatedDate).HasConversion<DateTime>();
        builder.Property(x => x.UpdatedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.UpdatedDate).HasConversion<DateTime>();

        builder.HasMany(x => x.Members).WithOne(m => m.Project).HasForeignKey(m => m.ProjectId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(pm => pm.Workspace).WithMany(p => p.Projects).HasForeignKey(pm => pm.WorkspaceId).OnDelete(DeleteBehavior.Restrict);
    }
}
