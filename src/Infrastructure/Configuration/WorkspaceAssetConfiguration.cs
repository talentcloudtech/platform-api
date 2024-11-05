using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Configuration for the WorkspaceAsset entity.
/// </summary>
public class WorkspaceAssetConfiguration : IEntityTypeConfiguration<WorkspaceAsset>
{
    /// <summary>
    /// Configures the entity and its relationships.
    /// </summary>
    /// <param name="builder">The builder to configure the entity.</param>
    public void Configure(EntityTypeBuilder<WorkspaceAsset> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(25).HasConversion<WorkspaceAssetId.EfCoreValueConverter>();
        builder.Property(x => x.WorkspaceId).HasMaxLength(25).HasConversion<WorkspaceId.EfCoreValueConverter>();
        builder.Property(x => x.ProjectId).HasMaxLength(25).HasConversion<ProjectId.EfCoreValueConverter>();
        builder.Property(x => x.TaskId).HasMaxLength(25).HasConversion<ProjectTaskId.EfCoreValueConverter>();

        builder.Property(x => x.FileUrl).HasMaxLength(2048).IsRequired();
        builder.Property(x => x.FileType).HasMaxLength(256).IsRequired();
        builder.Property(x => x.FileSize).IsRequired();
        builder.Property(x => x.Scope).HasConversion<string>().IsRequired();

        builder.Property(x => x.CreatedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.CreatedDate).HasConversion<DateTime>();
        builder.Property(x => x.UpdatedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.UpdatedDate).HasConversion<DateTime>();

        builder.HasOne(x => x.Workspace).WithMany(w => w.WorkspaceAssets).HasForeignKey(x => x.WorkspaceId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Project).WithMany(p => p.WorkspaceAssets).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Task).WithMany(t => t.WorkspaceAssets).HasForeignKey(x => x.TaskId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(w => w.User).WithMany(u => u.WorkspaceAssets).HasForeignKey(w => w.CreatedBy).OnDelete(DeleteBehavior.Restrict);
    }
}
