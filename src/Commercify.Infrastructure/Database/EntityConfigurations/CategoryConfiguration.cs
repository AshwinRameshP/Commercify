using Commercify.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Commercify.Infrastructure.Database.EntityConfigurations;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).HasMaxLength(Category.MaxLengths.Name);
        builder.Property(x => x.Description).HasMaxLength(Category.MaxLengths.Description);
        //Has to be fixed date, so it doesn't trigger update on every migration run
        var currentTime = new DateTime(2024, 11, 28, 6, 0, 0, DateTimeKind.Utc);

        builder.HasData(
            new Category
            {
                Id = 1,
                Name = "Electronics",
                Description = "Devices and gadgets for everyday use",
                CreatedAt = currentTime,
                LastUpdatedAt = currentTime
            },
            new Category
            {
                Id = 2,
                Name = "Clothing",
                Description = "Apparel for men, women, and children",
                CreatedAt = currentTime,
                LastUpdatedAt = currentTime
            },
            new Category
            {
                Id = 3,
                Name = "Home Appliances",
                Description = "Appliances to improve home living",
                CreatedAt = currentTime,
                LastUpdatedAt = currentTime
            },
            new Category
            {
                Id = 4,
                Name = "Books",
                Description = "Literature across genres and interests",
                CreatedAt = currentTime,
                LastUpdatedAt = currentTime
            },
            new Category
            {
                Id = 5,
                Name = "Toys",
                Description = "Toys and games for children of all ages",
                CreatedAt = currentTime,
                LastUpdatedAt = currentTime
            }
        );
    }
}
