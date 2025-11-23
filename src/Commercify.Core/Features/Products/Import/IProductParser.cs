using Commercify.Core.Models;

namespace Commercify.Core.Features.Products.Import;

public interface IProductParser
{
    Result<List<ProductImportRequest>> GetImportedProducts(UploadedFile csvFile);
}