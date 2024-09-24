using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Configures the WorkspaceMemberInvitation entity.
/// </summary>
public class WorkspaceMemberInvitationConfiguration : IEntityTypeConfiguration<WorkspaceMemberInvitation>
{
    /// <summary>
    /// Configures the WorkspaceMemberInvitation entity.
    /// </summary>
    /// <param name="builder">The builder to configure the entity.</param>
    public void Configure(EntityTypeBuilder<WorkspaceMemberInvitation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(StringSizes.Id).HasConversion<WorkspaceMemberInvitationId.EfCoreValueConverter>();
        builder.Property(x => x.WorkspaceId).HasMaxLength(StringSizes.Id).HasConversion<WorkspaceId.EfCoreValueConverter>();
        builder.Property(x => x.Email).HasMaxLength(StringSizes.Email);
        builder.Property(x => x.Code).HasMaxLength(12);
        builder.Property(x => x.Role).HasConversion<string>();
        builder.Property(x => x.CreatedBy).HasMaxLength(StringSizes.Id).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.CreatedDate).HasConversion<DateTime>();
        builder.Property(x => x.UpdatedBy).HasMaxLength(StringSizes.Id).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.UpdatedDate).HasConversion<DateTime>();

        builder.HasOne(wm => wm.Workspace).WithMany(w => w.WorkspaceMemberInvitations).HasForeignKey(wm => wm.WorkspaceId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(wm => wm.User).WithMany(u => u.WorkspaceMemberInvitations).HasForeignKey(wm => wm.CreatedBy).OnDelete(DeleteBehavior.Restrict);
    }
}
