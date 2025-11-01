namespace Commercify.Core.Features.Products.Import;

public record ProductImportRequest(string Name, string Description, decimal Price, long CategoryId, int StockQuantity);