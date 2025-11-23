namespace Commercify.Core.Features.Products.Read;

public record ProductResponse(long Id, string Name, string Description, decimal Price, long CategoryId, string CategoryName, int StockQuantity);