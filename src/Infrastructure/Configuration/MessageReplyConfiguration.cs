using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Configuration for the MessageReply entity.
/// </summary>
public class MessageReplyConfiguration : IEntityTypeConfiguration<MessageReply>
{
    /// <summary>
    /// Configures the entity and its relationships.
    /// </summary>
    /// <param name="builder">The builder to configure the entity.</param>
    public void Configure(EntityTypeBuilder<MessageReply> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(25).HasConversion<MessageReplyId.EfCoreValueConverter>();
        builder.Property(x => x.OriginalMessageId).HasMaxLength(25).HasConversion<MessageId.EfCoreValueConverter>();
        builder.Property(x => x.SenderId).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.Body).HasMaxLength(StringSizes.Max).IsRequired();

        builder.HasOne(mr => mr.OriginalMessage).WithMany().HasForeignKey(mr => mr.OriginalMessageId).OnDelete(DeleteBehavior.Restrict);
    }
}
