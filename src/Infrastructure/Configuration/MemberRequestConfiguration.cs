using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using System;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Configuration for the <see cref="MemberRequest"/> entity.
/// </summary>
public class MemberRequestConfiguration : IEntityTypeConfiguration<MemberRequest>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<MemberRequest> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(StringSizes.Id).HasConversion<MemberRequestId.EfCoreValueConverter>();
        builder.Property(x => x.Title).HasMaxLength(StringSizes.Title);
        builder.Property(x => x.Description).HasMaxLength(StringSizes.Max);
        builder.Property(x => x.RequiredSkills).HasMaxLength(StringSizes.Max);
        builder.Property(x => x.Timezone).HasMaxLength(StringSizes.Short).IsRequired();
        builder.Property(x => x.ExpectedBudget).HasColumnType("decimal(18, 2)");
        builder.Property(x => x.MinutesPerDay).HasDefaultValue(0);
        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
        builder.Property(x => x.Status).IsRequired();

        builder.Property(x => x.CreatedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.CreatedDate).HasConversion<DateTime>();
        builder.Property(x => x.UpdatedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.UpdatedDate).HasConversion<DateTime>();

        builder.HasOne(x => x.Workspace).WithMany(x => x.MemberRequests).HasForeignKey(x => x.WorkspaceId);
        builder.HasMany(x => x.Invitations).WithOne(x => x.MemberRequest).HasForeignKey(x => x.MemberRequestId);
    }
}