using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Infrastructure.Configuration;

/// <summary>
/// Configuration for the MessageAttachment entity.
/// </summary>
public class MessageAttachmentConfiguration : IEntityTypeConfiguration<MessageAttachment>
{
    /// <summary>
    /// Configures the entity and its relationships.
    /// </summary>
    /// <param name="builder">The builder to configure the entity.</param>
    public void Configure(EntityTypeBuilder<MessageAttachment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(25).HasConversion<MessageAttachmentId.EfCoreValueConverter>();
        builder.Property(x => x.MessageId).HasMaxLength(25).HasConversion<MessageId.EfCoreValueConverter>();
        builder.Property(x => x.ReplyId).HasMaxLength(25).HasConversion<MessageReplyId.EfCoreValueConverter>();
        builder.Property(x => x.FileName).HasMaxLength(StringSizes.FileName).IsRequired();
        builder.Property(x => x.FileUrl).HasMaxLength(StringSizes.Url).IsRequired();
        builder.Property(x => x.FileType).HasMaxLength(StringSizes.FileType).IsRequired();
        builder.Property(x => x.UploadedBy).HasMaxLength(25).HasConversion<UserId.EfCoreValueConverter>();
        builder.Property(x => x.ProjectId).HasMaxLength(25).HasConversion<ProjectId.EfCoreValueConverter>();
        builder.Property(x => x.WorkspaceId).HasMaxLength(25).HasConversion<WorkspaceId.EfCoreValueConverter>();

        builder.HasOne(ma => ma.Message)
            .WithMany()
            .HasForeignKey(ma => ma.MessageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ma => ma.Reply)
            .WithMany()
            .HasForeignKey(ma => ma.ReplyId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}
