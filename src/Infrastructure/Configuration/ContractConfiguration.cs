using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Configuration for the Contract entity.
/// </summary>
public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    /// <summary>
    /// Configures the entity and its relationships.
    /// </summary>
    /// <param name="builder">The builder to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(25).HasConversion<ContractId.EfCoreValueConverter>();
        builder.Property(x => x.UserId).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.WorkspaceId).HasMaxLength(25).HasConversion<WorkspaceId.EfCoreValueConverter>();
        builder.Property(x => x.BillingType).HasConversion<string>();
        builder.Property(x => x.Status).HasConversion<string>();

        builder.Property(x => x.CreatedBy).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.UpdatedBy).HasConversion<UserId.EfCoreValueConverter>();
    }
}