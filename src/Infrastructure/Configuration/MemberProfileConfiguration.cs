using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using Platform.Infrastructure.Converters;

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

        builder.Property(x => x.Id).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.FullName).HasMaxLength(StringSizes.Title);
        builder.Property(x => x.UserName).HasMaxLength(32);
        builder.Property(x => x.ShortBio).HasMaxLength(StringSizes.Bio);
        builder.Property(x => x.Subtitle).HasMaxLength(StringSizes.Title);
        builder.Property(x => x.ProfilePictureUrl).HasMaxLength(StringSizes.Url);
        builder.Property(x => x.Timezone).HasMaxLength(50);
        builder.Property(x => x.Address).HasMaxLength(StringSizes.Bio);
        builder.Property(x => x.ResumeFileUrl).HasMaxLength(StringSizes.Url);
        builder.Property(x => x.IsProfileComplete).HasDefaultValue(false);

        builder.OwnsMany(
            x => x.Languages,
            x =>
            {
                x.ToJson();
            }
        );

        builder.OwnsMany(
            x => x.Skills,
            x =>
            {
                x.ToJson();
                x.Property(s => s.SkillId).HasConversion(v => v.Value, v => new SkillId(v));
            }
        );
        builder.OwnsMany(
            x => x.Experiences,
            x =>
            {
                x.ToJson();
            }
        );

        builder.OwnsMany(
            x => x.Educations,
            x =>
            {
                x.ToJson();
            }
        );

        builder.OwnsMany(
            x => x.Certifications,
            x =>
            {
                x.ToJson();
            }
        );

        builder.OwnsMany(
            x => x.ProjectInfos,
            x =>
            {
                x.ToJson();
                x.Property(p => p.Technologies).HasConversion(new ShortSkillResponseListConverter());
            }
        );

        builder.Property(x => x.ExternalLinks).HasColumnType("text[]").IsRequired(false);
    }
}
