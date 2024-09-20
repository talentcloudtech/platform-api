using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using System;
using System.Linq;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Configuration for the Workspace entity.
/// </summary>
public class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
{
    /// <summary>
    /// Configures the entity type for the Workspace entity.
    /// </summary>
    /// <param name="builder">The builder to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<Workspace> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(25).HasConversion<WorkspaceId.EfCoreValueConverter>();
        builder.Property(x => x.Name).HasMaxLength(StringSizes.Title).IsRequired();
        builder.Property(x => x.ContactEmail).HasMaxLength(StringSizes.Email).IsRequired();
        builder.Property(x => x.Address).HasMaxLength(StringSizes.ShortAddress);
        builder.Property(x => x.Logo).HasMaxLength(StringSizes.Url);

        builder.Property(x => x.Urls).HasConversion(
            v => string.Join(',', v),
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
        );

        builder.HasMany(w => w.Members)
            .WithOne(m => m.Workspace)
            .HasForeignKey(m => m.WorkspaceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}