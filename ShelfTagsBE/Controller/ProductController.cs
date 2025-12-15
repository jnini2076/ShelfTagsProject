using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShelfTagsBE.Dto;
using ShelfTagsBE.Service;
using ShelfTagsBE.Models;
using Microsoft.AspNetCore.HttpLogging;

namespace ShelfTagsBE.Controller
{
    [Route("api/[controller]")]
    [ApiController]
        public class ProductController : ControllerBase
    {
        
        private readonly ProductService productService;

        public ProductController(ProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost("productpost")]

        public async Task<IActionResult> PostProduct([FromBody]ProductDTO productRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             var product = new Product
                {
                        Name = productRequest.Name,
                        SKU = productRequest.SKU,
                        Brand = productRequest.Brand,
                        Category = productRequest.Category,
                        CurrentPrice = productRequest.CurrentPrice,
                        DefaultBarcodeType = (ShelfTagsBE.Models.BarcodeType)productRequest.DefaultBarcodeType,
                        CreatedAt = DateTime.UtcNow ,
                        UpdatedAt = DateTime.UtcNow 
                };
            await productService.PostProduct(product);
            return Ok(product);
    }

        [HttpGet]

         public async Task<IActionResult> GetAllProducts()
        {
            var products = await productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetProductByName(string productName)
        {
            var findName = await productService.GetProductbyName(productName);

            return Ok(findName);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id ,[FromBody] UpdateProductRequest updateProductRequest)
        {
           

            var updateProduct =  await productService.UpdateProduct(id,updateProductRequest.NewPrice);

            return Ok(updateProduct);
        }
        
        [HttpGet("{id}")]

        public async Task<IActionResult> GetProductById(int id)
        {
            var getproduct = await productService.GetProductById(id);

            if(getproduct == null)
            {
                return NotFound();            
            }
            return Ok(getproduct);
        }

        [HttpGet("{id}history")]

        public async Task<IActionResult> GetPriceHistory(int id)
        {
            var history = await productService.GetPriceHistories(id);

            return Ok(history);
        }


    }
}