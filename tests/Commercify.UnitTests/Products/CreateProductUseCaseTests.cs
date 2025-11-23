using Commercify.Core.Features.Products.Create;
using Commercify.Core.Models;
using Commercify.Core.Shared;
using Commercify.UnitTests.TestSetup;
using FluentAssertions;

namespace Commercify.UnitTests.Products;

public class CreateProductUseCaseTests
{
    [Fact]
    public async Task Product_is_not_created_when_category_does_not_exist()
    {
        //Arrange
        using var builder = new DatabaseBuilder();
        var context = builder.CreateDbContext();
        CreateProductUseCase createProductUseCase = CreateUseCase(context);
        var request = new CreateProductRequest("Product 1", "Description 1", 100, 99, 10);

        //Act
        Result<CreateProductResponse> result = await createProductUseCase.Execute(request);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.NotFound);
    }
    [Fact]
    public async Task Product_is_created_when_category_exists()
    {
        //Arrange
        using var builder = new DatabaseBuilder();
        var context = builder.CreateDbContext();
        CreateProductUseCase createProductUseCase = CreateUseCase(context);
        var request = new CreateProductRequest("Product 1", "Description 1", 100, 1, 10);

        //Act
        Result<CreateProductResponse> result = await createProductUseCase.Execute(request);

        //Assert
        result.IsSuccess.Should().BeTrue();
        var createdProduct = result.Value;
        createdProduct.Id.Should().BeGreaterThan(0);
        createdProduct.Name.Should().Be("Product 1");
        createdProduct.Description.Should().Be("Description 1");
        createdProduct.Price.Should().Be(100);
        createdProduct.CategoryId.Should().Be(1);
        createdProduct.CategoryName.Should().Be("Electronics");
        createdProduct.StockQuantity.Should().Be(10);
    }

    private static CreateProductUseCase CreateUseCase(IDbContext context)
    {
        return new CreateProductUseCase(context);
    }
}
