using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Configuration for the MessageRecipient entity.
/// </summary>
public class MessageRecipientConfiguration : IEntityTypeConfiguration<MessageRecipient>
{
    /// <summary>
    /// Configures the entity and its relationships.
    /// </summary>
    /// <param name="builder">The builder to configure the entity.</param>
    public void Configure(EntityTypeBuilder<MessageRecipient> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(25).HasConversion<MessageRecipientId.EfCoreValueConverter>();
        builder.Property(x => x.MessageId).HasMaxLength(25).HasConversion<MessageId.EfCoreValueConverter>();
        builder.Property(x => x.RecipientId).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();

        builder.HasOne(mr => mr.Message).WithMany(m => m.Recipients).HasForeignKey(mr => mr.MessageId).OnDelete(DeleteBehavior.Restrict);
    }
}
