using Commercify.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Commercify.Infrastructure.Database.EntityConfigurations;
public class ProductConfiguration
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.ToTable("Products");
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Name).HasMaxLength(Product.MaxLengths.Name);
		builder.Property(x => x.Description).HasMaxLength(Product.MaxLengths.Description);
		builder.HasOne(x=>x.Category).WithMany(x=>x.Products)
			.HasForeignKey(x=>x.CategoryId).OnDelete(DeleteBehavior.Restrict);
	}
}
