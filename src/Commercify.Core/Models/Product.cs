namespace Commercify.Core.Models;

public class Product : BaseEntity
{
    public static class MaxLengths
    {
        public const int Name = 100;
        public const int Description = 500;
    }
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public long CategoryId {  get; set; }
    public Category? Category { get; set; } 
    public int stockQuantity { get; set; }
}