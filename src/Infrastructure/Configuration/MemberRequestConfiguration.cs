using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Configures the MemberRequest entity.
/// </summary>
public class MemberRequestConfiguration : IEntityTypeConfiguration<MemberRequest>
{
    /// <summary>
    /// Configures the entity and its relationships.
    /// </summary>
    /// <param name="builder">The builder to configure the entity.</param>
    public void Configure(EntityTypeBuilder<MemberRequest> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(StringSizes.Id).HasConversion<MemberRequestId.EfCoreValueConverter>();
        builder.Property(x => x.WorkspaceId).HasMaxLength(StringSizes.Id).HasConversion<WorkspaceId.EfCoreValueConverter>();
        builder.Property(x => x.Title).HasMaxLength(StringSizes.Title);
        builder.Property(x => x.Description).HasMaxLength(StringSizes.Max);
        builder.OwnsMany(
            x => x.RequiredSkills,
            x =>
            {
                x.ToJson();
                x.Property(s => s.SkillId).HasConversion(v => v.Value, v => new SkillId(v));
            }
        );
        builder.Property(x => x.StartDate).HasConversion<DateTime>();
        builder.Property(x => x.EndDate).HasConversion<DateTime>();
        builder.Property(x => x.ExpectedBudget).HasMaxLength(StringSizes.Title);
        builder.Property(x => x.WorkHour).HasMaxLength(StringSizes.Title);
        builder.Property(x => x.Timezone).HasMaxLength(StringSizes.Short);
        builder.Property(x => x.Status).HasConversion<string>();
        builder.Property(x => x.RejectedReason).HasMaxLength(300);
        builder.Property(x => x.Type).HasConversion<string>();
        builder.Property(x => x.Visibility).HasConversion<string>();
        builder.Property(x => x.CreatedBy).HasMaxLength(StringSizes.Id).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.CreatedDate).HasConversion<DateTime>();
        builder.Property(x => x.UpdatedBy).HasMaxLength(StringSizes.Id).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.UpdatedDate).HasConversion<DateTime>();

        builder.HasOne(mr => mr.Workspace).WithMany(w => w.MemberRequests).HasForeignKey(mr => mr.WorkspaceId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(mr => mr.User).WithMany(u => u.MemberRequests).HasForeignKey(mr => mr.CreatedBy).OnDelete(DeleteBehavior.Restrict);
    }
}
