using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Represents the configuration for the 'ApplicationUser' entity.
/// </summary>
public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();

        builder.HasOne(x => x.MemberProfile).WithOne(p => p.User).HasForeignKey<MemberProfile>(p => p.Id);
        builder.HasMany(x => x.MemberRequestInvitations).WithOne(p => p.InvitedUser).HasForeignKey(p => p.InvitedUserId);
        builder.HasMany(x => x.ProjectMembers).WithOne(p => p.User).HasForeignKey(p => p.UserId);
        builder.HasMany(x => x.TaskAssignments).WithOne(p => p.User).HasForeignKey(p => p.UserId);
        builder.HasMany(x => x.Contracts).WithOne(p => p.User).HasForeignKey(p => p.UserId);
    }
}
