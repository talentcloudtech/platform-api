using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Services;

/// <summary>
/// Represents the configuration for the 'RefreshToken' entity.
/// </summary>
public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    /// <summary>
    /// Configures the entity type for the RefreshToken class.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>s
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(25).HasConversion<RefreshTokenId.EfCoreValueConverter>();
        builder.Property(x => x.Token).HasMaxLength(64);
        builder.Property(x => x.Expires).IsRequired();
        builder.Property(x => x.Created).IsRequired();
        builder.Property(x => x.UserId).IsRequired();

        builder.HasOne(x => x.User).WithMany(x => x.RefreshTokens).HasForeignKey(x => x.UserId);
    }
}
