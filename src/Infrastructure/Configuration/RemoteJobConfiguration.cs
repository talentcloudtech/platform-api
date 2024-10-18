using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using System;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Configuration for the RemoteJob entity.
/// </summary>
public class RemoteJobConfiguration : IEntityTypeConfiguration<RemoteJob>
{
    /// <summary>
    /// Configures the entity and its relationships.
    /// </summary>
    public void Configure(EntityTypeBuilder<RemoteJob> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(25).HasConversion<RemoteJobId.EfCoreValueConverter>();
        builder.Property(x => x.Title).HasMaxLength(StringSizes.Title).IsRequired();
        builder.Property(x => x.CompanyName).HasMaxLength(StringSizes.Title).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(StringSizes.Max).IsRequired();
        builder.Property(x => x.Category).HasMaxLength(StringSizes.Title);
        builder.Property(x => x.Location).HasMaxLength(StringSizes.Title);
        builder.Property(x => x.SalaryRange).HasMaxLength(StringSizes.Title);
        builder.Property(x => x.JobType).HasMaxLength(StringSizes.Title);
        builder.Property(x => x.ApplyUrl).HasMaxLength(StringSizes.Url);
        builder.Property(x => x.Status).HasMaxLength(StringSizes.Title);

        builder.Property(x => x.CreatedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.CreatedDate).HasConversion<DateTime>();
        builder.Property(x => x.UpdatedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.UpdatedDate).HasConversion<DateTime>();
    }
}
