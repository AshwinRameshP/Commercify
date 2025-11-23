namespace Commercify.Core.Features.Products.Create;

public record CreateProductRequest(string Name, string Description, decimal Price, long CategoryId, int StockQuantity);

public record CreateProductResponse(long Id, string Name, string Description, decimal Price, long CategoryId, string CategoryName, int StockQuantity);