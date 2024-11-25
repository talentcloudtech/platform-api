using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Configuration for the Message entity.
/// </summary>
public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    /// <summary>
    /// Configures the entity and its relationships.
    /// </summary>
    /// <param name="builder">The builder to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(25).HasConversion<MessageId.EfCoreValueConverter>();
        builder.Property(x => x.SenderId).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.ProjectId).HasMaxLength(25).HasConversion<ProjectId.EfCoreValueConverter>();
        builder.Property(x => x.WorkspaceId).HasMaxLength(25).HasConversion<WorkspaceId.EfCoreValueConverter>();
        builder.Property(x => x.Subject).HasMaxLength(StringSizes.Title).IsRequired();
        builder.Property(x => x.Body).HasMaxLength(StringSizes.Max).IsRequired();
        builder.Property(x => x.MessageType).HasConversion<string>();
        builder.Property(x => x.ReplyAllowed);
        builder.Property(x => x.CreatedAt).HasConversion<DateTime>();
        builder.Property(x => x.RecipientCount);
        builder.Property(x => x.TotalReplyCount);

        builder.HasOne(m => m.Sender).WithMany(s => s.SentMessages).HasForeignKey(m => m.SenderId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(m => m.Recipients).WithOne(r => r.Message).HasForeignKey(r => r.MessageId).OnDelete(DeleteBehavior.Cascade);
    }
}
