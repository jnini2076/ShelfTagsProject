using System;
using ShelfTagsBE.Models;

namespace ShelfTagsBE.Repos;

public interface IProductInterface
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int productId);
    Task<Product?> GetProductAsync(string productName);
    Task<Product?> FindByNameAsync(string name);
    Task<Product?> FindBySKUAsync(string sku);

    Task<Product> CreateProductAsync(Product product);
    Task AddPriceHistoryAsync(PriceHistory priceHistory);

    Task<Product> UpdateProductPriceAsync(int productId, decimal newPrice);

    Task<List<PriceHistory>> GetPriceHistoriesAsync(int productId);
}
