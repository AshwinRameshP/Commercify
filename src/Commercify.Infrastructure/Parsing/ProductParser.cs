using Commercify.Core.Features.Products.Import;
using Commercify.Core.Models;
using CsvHelper;
using System.Globalization;

namespace Commercify.Infrastructure.Parsing;

public class ProductParser: IProductParser
{
    public Result<List<ProductImportRequest>> GetImportedProducts(UploadedFile csvFile)
    {
        try
        {
            using var memoryStream = new MemoryStream(csvFile.FileData);
            using var reader = new StreamReader(memoryStream);

            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            var productsToImport = csvReader.GetRecords<ProductImportRequest>().ToList();
            return productsToImport;
        }
        catch(CsvHelperException ex)
        {
            return Result.Error($"Data import error: {ex.Message}");
        }

    }
}
