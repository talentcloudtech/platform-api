using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using System;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Configuration for the GigApplicantInvitation entity.
/// </summary>
public class GigApplicantInvitationConfiguration : IEntityTypeConfiguration<GigApplicantInvitation>
{
    /// <summary>
    /// Configures the entity and its relationships.
    /// </summary>
    /// <param name="builder">The builder to configure the entity.</param>
    public void Configure(EntityTypeBuilder<GigApplicantInvitation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasMaxLength(25)
            .HasConversion<GigApplicantInvitationId.EfCoreValueConverter>();

        builder.Property(x => x.MemberRequestId)
            .HasMaxLength(25)
            .HasConversion<MemberRequestId.EfCoreValueConverter>().IsRequired();

        builder.Property(x => x.InvitedUserId)
            .HasMaxLength(25)
            .HasConversion<UserId.EfCoreValueConverter>().IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>().IsRequired();

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(25)
            .HasConversion<UserId.EfCoreValueConverter>().IsRequired();

        builder.Property(x => x.CreatedDate)
            .HasConversion<DateTime>().IsRequired();

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(25)
            .HasConversion<UserId.EfCoreValueConverter>();

        builder.Property(x => x.UpdatedDate)
            .HasConversion<DateTime>();

        // Configure relationships
        builder.HasOne(gai => gai.MemberRequest)
            .WithMany(mr => mr.Invitations)
            .HasForeignKey(gai => gai.MemberRequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(gai => gai.InvitedUser)
            .WithMany(u => u.GigApplicantInvitations)
            .HasForeignKey(gai => gai.InvitedUserId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasOne(gai => gai.CreatedByUser)
            .WithMany()
            .HasForeignKey(gai => gai.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
