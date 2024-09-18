using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Represents the configuration for the skill entity.
/// </summary>
public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    /// <summary>
    /// Configures the entity type for the Skill class.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(25).HasConversion<SkillId.EfCoreValueConverter>();
        builder.Property(x => x.Name).HasMaxLength(StringSizes.Medium).IsRequired();
        builder.Property(x => x.Key).HasMaxLength(StringSizes.Medium).IsRequired(false);
        builder.Property(x => x.Path).HasMaxLength(StringSizes.Bio);
        builder.Property(x => x.Keywords).HasColumnType("text[]").IsRequired(false);

        builder.HasIndex(x => x.Name).IsUnique();
    }
}
