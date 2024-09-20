using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Configuration for the WorkspaceMember entity.
/// </summary>
public class WorkspaceMemberConfiguration : IEntityTypeConfiguration<WorkspaceMember>
{
    /// Configures the entity and its relationships.
    /// </summary>
    /// <param name="builder">The builder to configure the entity.</param>
    public void Configure(EntityTypeBuilder<WorkspaceMember> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(25).HasConversion<WorkspaceMemberId.EfCoreValueConverter>();
        builder.Property(x => x.UserId).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.WorkspaceId).HasMaxLength(25).HasConversion<WorkspaceId.EfCoreValueConverter>();
        builder.Property(x => x.Role).HasConversion<string>();

        builder.HasOne(wm => wm.Workspace)
            .WithMany(w => w.Members)
            .HasForeignKey(wm => wm.WorkspaceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}