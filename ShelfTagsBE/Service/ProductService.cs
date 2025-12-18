using System;
using Microsoft.Extensions.Caching.Memory;
using ShelfTagsBE.Models;
using ShelfTagsBE.Repos;

namespace ShelfTagsBE.Service;

public class ProductService
{
    private readonly IProductInterface productRepository;
    private readonly ILogger<ProductService> logger;
    private readonly IMemoryCache cache;

    public ProductService(IProductInterface productRepository, ILogger<ProductService> logger, IMemoryCache cache)
    {
        this.productRepository = productRepository;
        this.logger = logger;
        this.cache = cache;
    }

    public async Task<Product> PostProduct(Product product)
    {
        var findname = await productRepository.FindByNameAsync(product.Name);

        if(findname != null)
        {
            logger.LogWarning("product name already exists in the database");
            throw new InvalidOperationException("product already exist");
        }

        var findSKU = await productRepository.FindBySKUAsync(product.SKU);

        if(findSKU != null)
        {
            logger.LogWarning("These SKUS has already been made");
            throw new InvalidOperationException("SKU already exists");
        }

        var createProduct = await productRepository.CreateProductAsync(product);
        logger.LogInformation("product has been created");
        return createProduct;
        
            
        
    }


    public async Task<List<Product>> GetAllProducts()
    {
       var GetALLPRODUCTS =  await productRepository.GetAllProductsAsync();

        if (GetALLPRODUCTS == null)
        {
            logger.LogWarning("for some reason we cannot get all the products");
            throw new InvalidOperationException("cant find any products");
        }
       logger.LogInformation("we have retrieved all the available products in the system");
       return GetALLPRODUCTS;
       
    }

    public async Task<Product?> GetProductbyName(string productName)
    {
            var cacheKey = $"{productName}";

            if(cache.TryGetValue(cacheKey, out Product? cachedProduct))
            {
                 logger.LogInformation($"cache hit for product: {productName}");
                    return cachedProduct;
                 
            }

            logger.LogInformation($"cache missed for product  {productName}");

            




        var getProduct = await productRepository.GetProductAsync(productName);

        if (getProduct == null)
        {
            logger.LogWarning("cant find that specific product");
            throw new InvalidOperationException("cant find the product");
        }
        var cacheOptions = new MemoryCacheEntryOptions 
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(7)
        };
        cache.Set(cacheKey, getProduct, cacheOptions);

        logger.LogInformation("we have retrieved the product by the specific name ");
        return getProduct;
    }


    public async Task<Product?> UpdateProduct(int productId, decimal newPrice)
    {

        
        var currentProduct = await productRepository.GetProductByIdAsync(productId);

        if (currentProduct == null)
        {
            logger.LogError("the product id wasnt in the DB");
            throw new InvalidOperationException("product id wasnt in the DB");
        }

        var pricehistory = new PriceHistory
        {
            ProductId = productId,
            OldPrice = currentProduct.CurrentPrice,
            NewPrice = (double)newPrice,
            ChangedAT = DateTime.UtcNow
        };
        
        await productRepository.AddPriceHistoryAsync(pricehistory);
        var UpdateProduct = await productRepository.UpdateProductPriceAsync(productId,newPrice);
          
          
        
        return UpdateProduct;
    }   

    public async Task<Product?> GetProductById(int productId)

    {
        var GetproductbyId = await productRepository.GetProductByIdAsync(productId);
        if (GetproductbyId == null)
        {
            throw new InvalidOperationException("product cant be found");
        }
        return GetproductbyId ; 
    }


    public async Task <List<PriceHistory>> GetPriceHistories(int productId)
    {
        return await productRepository.GetPriceHistoriesAsync(productId);
    }
}


