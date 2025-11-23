using System.ComponentModel.DataAnnotations.Schema;

namespace Commercify.Core.Models;

[Table("Categories")]
public class Category : BaseEntity
{
    public static class MaxLengths
    {
        public const int Name = 100;
        public const int Description = 500;

    }
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
