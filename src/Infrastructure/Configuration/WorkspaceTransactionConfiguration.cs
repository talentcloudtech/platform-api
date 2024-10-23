using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Represents the configuration for the 'WorkspaceTransaction' entity.
/// </summary>
public class WorkspaceTransactionConfiguration : IEntityTypeConfiguration<WorkspaceTransaction>
{
    /// <summary>
    /// Configures the entity type for the WorkspaceTransaction class.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<WorkspaceTransaction> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(25).HasConversion<WorkspaceTransactionId.EfCoreValueConverter>();
        builder.Property(x => x.WorkspaceId).HasMaxLength(25).HasConversion<WorkspaceId.EfCoreValueConverter>();
        builder.Property(x => x.Type).HasConversion<string>();
        builder.Property(x => x.Amount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.Description).HasMaxLength(StringSizes.Bio);
        builder.Property(x => x.PaymentMethod).HasConversion<string>();
        builder.Property(x => x.TransactionId).HasMaxLength(StringSizes.Short);
        builder.Property(x => x.Status).HasConversion<string>();

        builder.OwnsMany(
            x => x.PayeeDetails,
            x =>
            {
                x.ToJson();
                x.Property(p => p.TaskId).HasConversion<ProjectTaskId.EfCoreValueConverter>();
                x.Property(p => p.ContractId).HasConversion<ContractId.EfCoreValueConverter>();
                x.Property(p => p.OpportunityId).HasConversion<MemberRequestId.EfCoreValueConverter>();
            }
        );

        builder.Property(x => x.CreatedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.CreatedDate).HasConversion<DateTime>();
        builder.Property(x => x.UpdatedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.UpdatedDate).HasConversion<DateTime>();

        builder.HasOne(x => x.User).WithMany(x => x.WorkspaceTransactions).HasForeignKey(x => x.CreatedBy).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Workspace).WithMany(w => w.WorkspaceTransactions).HasForeignKey(x => x.WorkspaceId).OnDelete(DeleteBehavior.Restrict);
    }
}
