using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Represents the configuration for the 'MemberTransaction' entity.
/// </summary>
public class MemberTransactionConfiguration : IEntityTypeConfiguration<MemberTransaction>
{
    /// <summary>
    /// Configures the entity type for the MemberTransaction class.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<MemberTransaction> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(25).HasConversion<MemberTransactionId.EfCoreValueConverter>();
        builder.Property(x => x.UserId).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.Type).HasConversion<string>();
        builder.Property(x => x.Amount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.Description).HasMaxLength(StringSizes.Bio);
        builder.Property(x => x.PaymentMethod).HasConversion<string>();
        builder.Property(x => x.TransactionId).HasMaxLength(StringSizes.Short);
        builder.Property(x => x.Status).HasConversion<string>();

        builder.OwnsMany(
            x => x.PayerDetails,
            x =>
            {
                x.ToJson();
                x.Property(p => p.WorkspaceId).HasConversion<WorkspaceId.EfCoreValueConverter>();
                x.Property(p => p.ProjectId).HasConversion<ProjectId.EfCoreValueConverter>();
                x.Property(p => p.ContractId).HasConversion<ContractId.EfCoreValueConverter>();
                x.Property(p => p.TaskId).HasConversion<ProjectTaskId.EfCoreValueConverter>();
            }
        );

        builder.Property(x => x.CreatedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.CreatedDate).HasConversion<DateTime>();
        builder.Property(x => x.UpdatedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.UpdatedDate).HasConversion<DateTime>();

        builder.HasOne(x => x.User).WithMany(x => x.MemberTransactions).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CreatedByUser).WithMany(x => x.CreatedMemberTransactions).HasForeignKey(x => x.CreatedBy).OnDelete(DeleteBehavior.Restrict);
    }
}
