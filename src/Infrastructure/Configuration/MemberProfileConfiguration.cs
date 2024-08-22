using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using System;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Represents the configuration for the 'Project' entity.
/// </summary>
public class MemberProfileConfiguration : IEntityTypeConfiguration<MemberProfile>
{
    /// <summary>
    /// Configures the entity type for the Project class.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<MemberProfile> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion<MemberProfileId.EfCoreValueConverter>();
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(StringSizes.Title);
        builder.Property(x => x.HandleName).IsRequired().HasMaxLength(20);
        builder.Property(x => x.ShortBio).IsRequired().HasMaxLength(StringSizes.Bio);
        builder.Property(x => x.ProfilePictureUrl).IsRequired().HasMaxLength(StringSizes.Url);
        builder.Property(x => x.Timezone).IsRequired().HasMaxLength(50);
        builder.Property(x => x.ResumeFileUrl).IsRequired().HasMaxLength(StringSizes.Url);

        builder.OwnsMany(
            x => x.Skills,
            x =>
            {
                x.ToJson();
            }
        );
        builder.OwnsMany(
            x => x.IndustryExperiences,
            x =>
            {
                x.ToJson();
            }
        );
        builder.Property(x => x.ExternalLinks).HasColumnType("text[]");
    }
}
