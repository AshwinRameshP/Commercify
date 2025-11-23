namespace Commercify.Core.Features.Products.Update;

public record UpdateProductRequest(string Name, string Description, decimal Price, long CategoryId, int StockQuantity);

public record UpdateProductResponse(long Id, string Name, string Description, decimal Price, long CategoryId, string CategoryName, int StockQuantity);