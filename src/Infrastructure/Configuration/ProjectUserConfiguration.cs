// using Domain.Entities;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using Platform.Domain.Entities.Common;

// namespace Platform.Infrastructure.Configuration;

// /// <summary>
// /// Represents the configuration for the 'ProjectUser' entity.
// /// </summary>
// public class ProjectUserConfiguration : IEntityTypeConfiguration<ProjectUser>
// {
//     /// <summary>
//     /// Configures the entity type for the ProjectUser class.
//     /// </summary>
//     /// <param name="builder">The entity type builder.</param>
//     public void Configure(EntityTypeBuilder<ProjectUser> builder)
//     {
//         builder.HasKey(x => new { x.UserId, x.ProjectId });

//         builder.Property(x => x.UserId).HasConversion<UserId.EfCoreValueConverter>();
//         builder.Property(x => x.ProjectId).HasConversion<ProjectId.EfCoreValueConverter>();

//         // modelBuilder.Entity<BookCategory>()
//         //     .HasOne(bc => bc.Category)
//         //     .WithMany(c => c.BookCategories)
//         //     .HasForeignKey(bc => bc.CategoryId);
//     }
// }
