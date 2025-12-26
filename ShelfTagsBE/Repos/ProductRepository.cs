using System;
using ShelfTagsBE.Models;
using ShelfTagsBE.Data;
using Microsoft.EntityFrameworkCore;

namespace ShelfTagsBE.Repos;

public class ProductRepository(DBmodel context) : IProductInterface
{
   

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await context.Products.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await context.Products.FindAsync(productId);
    }

    public async Task<Product?> GetProductAsync(string productName)
    {
        return await context.Products
            .FirstOrDefaultAsync(p => p.Name == productName);
    }

    public async Task<Product?> FindByNameAsync(string name)
    {
        return await context.Products
            .FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<Product?> FindBySKUAsync(string sku)
    {
        return await context.Products
            .FirstOrDefaultAsync(p => p.SKU == sku);
    }

    public async Task AddPriceHistoryAsync(PriceHistory priceHistory)
    {
        await context.PriceHistories.AddAsync(priceHistory);
        await context.SaveChangesAsync();
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateProductPriceAsync(int productId, decimal newPrice)
    {
        var product = await context.Products.FindAsync(productId);
        
        if (product == null)
            throw new Exception($"Product with ID {productId} not found");
        
        product.CurrentPrice = (double)newPrice;
        context.Products.Update(product);
        await context.SaveChangesAsync();
        
        return product;
    }

  public async Task<List<PriceHistory>> GetPriceHistoriesAsync(int productId)
{
    var priceHistories = await context.PriceHistories
        .Where(ph => ph.ProductId == productId)
        .ToListAsync();

    if (priceHistories.Count == 0)
    {
        throw new InvalidOperationException("There is no price history for this product yet");
    }

    return priceHistories;
}
}
