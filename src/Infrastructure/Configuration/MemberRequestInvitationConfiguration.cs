using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using System;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Configuration for the <see cref="MemberRequestInvitation"/> entity.
/// </summary>
public class MemberRequestInvitationConfiguration : IEntityTypeConfiguration<MemberRequestInvitation>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<MemberRequestInvitation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(StringSizes.Id).HasConversion<MemberRequestInvitationId.EfCoreValueConverter>();
        builder.Property(x => x.Status).IsRequired();

        builder.Property(x => x.CreatedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.CreatedDate).HasConversion<DateTime>();
        builder.Property(x => x.UpdatedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.UpdatedDate).HasConversion<DateTime>();

        builder.HasOne(x => x.InvitedUser).WithMany(x => x.MemberRequestInvitations).HasForeignKey(x => x.InvitedUserId);
        builder.HasOne(x => x.MemberRequest).WithMany(x => x.Invitations).HasForeignKey(x => x.MemberRequestId);

        // builder.HasIndex(x => x.InvitedUserId);
        // builder.HasIndex(x => x.MemberRequestId);

    }
}