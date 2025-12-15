using Xunit;
using Moq;
using ShelfTagsBE.Service;
using ShelfTagsBE.Repos;
using ShelfTagsBE.Models;
using ShelfTagsBE.Data;
using Microsoft.Extensions.Logging;

namespace ShelfTagsBE.Tests;

public class ProductServiceTests
{
    // This test verifies that PostProduct successfully creates a product when valid
    [Fact]
    public async Task PostProduct_CreatesProduct_WhenValidAndNoDuplicates()
    {
        // ARRANGE - Set up test data and mocks
        
        // Create a fake repository (mock)
        var mockRepository = new Mock<IProductInterface>();
        
        // Create a fake logger (mock)
        var mockLogger = new Mock<ILogger<ProductService>>();
        
        // Create a new product to be created
        var newProduct = new Product
        {
            Name = "NewProduct",
            SKU = "NEW123",
            Brand = "TestBrand",
            Category = "TestCategory",
            CurrentPrice = 15.99,
            DefaultBarcodeType = BarcodeType.Code128,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        // Setup: Return null for name check (no duplicate name)
        mockRepository
            .Setup(repo => repo.FindByNameAsync("NewProduct"))
            .ReturnsAsync((Product?)null);
        
        // Setup: Return null for SKU check (no duplicate SKU)
        mockRepository
            .Setup(repo => repo.FindBySKUAsync("NEW123"))
            .ReturnsAsync((Product?)null);
        
        // Setup: Return the created product when CreateProductAsync is called
        mockRepository
            .Setup(repo => repo.CreateProductAsync(It.IsAny<Product>()))
            .ReturnsAsync(newProduct);
        
        // Create the service with our mocks (NO CONTEXT!)
        var service = new ProductService(
            mockRepository.Object,
            mockLogger.Object
        );
        
        // ACT - Call the method we're testing
        var result = await service.PostProduct(newProduct);
        
        // ASSERT - Verify the result
        Assert.NotNull(result);
        Assert.Equal("NewProduct", result.Name);
        Assert.Equal("NEW123", result.SKU);
        
        // Verify that CreateProductAsync was called exactly once
        mockRepository.Verify(
            repo => repo.CreateProductAsync(It.IsAny<Product>()), 
            Times.Once
        );
    }
    
    // This test verifies that PostProduct throws exception when product name already exists
    [Fact]
    public async Task PostProduct_ThrowsException_WhenProductNameExists()
    {
        // ARRANGE
        var mockRepository = new Mock<IProductInterface>();
        var mockLogger = new Mock<ILogger<ProductService>>();
        
        // Create a product that already exists (duplicate)
        var existingProduct = new Product
        {
            Id = 1,
            Name = "ExistingProduct",
            SKU = "EXIST123",
            Brand = "TestBrand",
            Category = "TestCategory",
            CurrentPrice = 10.99,
            DefaultBarcodeType = BarcodeType.Code128,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        // Setup: Return existing product for name check (duplicate found!)
        mockRepository
            .Setup(repo => repo.FindByNameAsync("ExistingProduct"))
            .ReturnsAsync(existingProduct);
        
        var service = new ProductService(
            mockRepository.Object,
            mockLogger.Object
        );
        
        // Create a new product with the same name
        var newProduct = new Product
        {
            Name = "ExistingProduct",  // Same name = duplicate!
            SKU = "NEW456",
            Brand = "TestBrand",
            Category = "TestCategory",
            CurrentPrice = 12.99,
            DefaultBarcodeType = BarcodeType.Code128,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        // ACT & ASSERT - Expect exception to be thrown
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            async () => await service.PostProduct(newProduct)
        );
        
        // Verify the exception message
        Assert.Equal("product already exist", exception.Message);
        
        // Verify that CreateProductAsync was NEVER called (because validation failed)
        mockRepository.Verify(
            repo => repo.CreateProductAsync(It.IsAny<Product>()), 
            Times.Never
        );
    }
    
    // This test verifies that PostProduct throws exception when SKU already exists
    [Fact]
    public async Task PostProduct_ThrowsException_WhenSKUExists()
    {
        // ARRANGE
        var mockRepository = new Mock<IProductInterface>();
        var mockLogger = new Mock<ILogger<ProductService>>();
        
        var existingProduct = new Product
        {
            Id = 2,
            Name = "OtherProduct",
            SKU = "DUP123",
            Brand = "TestBrand",
            Category = "TestCategory",
            CurrentPrice = 8.99,
            DefaultBarcodeType = BarcodeType.Code128,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        // Setup: Return null for name check (name is unique)
        mockRepository
            .Setup(repo => repo.FindByNameAsync("NewProduct"))
            .ReturnsAsync((Product?)null);
        
        // Setup: Return existing product for SKU check (duplicate SKU!)
        mockRepository
            .Setup(repo => repo.FindBySKUAsync("DUP123"))
            .ReturnsAsync(existingProduct);
        
        var service = new ProductService(
            mockRepository.Object,
            mockLogger.Object
        );
        
        var newProduct = new Product
        {
            Name = "NewProduct",
            SKU = "DUP123",  // Duplicate SKU!
            Brand = "TestBrand",
            Category = "TestCategory",
            CurrentPrice = 9.99,
            DefaultBarcodeType = BarcodeType.Code128,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        // ACT & ASSERT
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            async () => await service.PostProduct(newProduct)
        );
        
        Assert.Equal("SKU already exists", exception.Message);
        
        // Verify CreateProductAsync was never called
        mockRepository.Verify(
            repo => repo.CreateProductAsync(It.IsAny<Product>()), 
            Times.Never
        );
    }
}
