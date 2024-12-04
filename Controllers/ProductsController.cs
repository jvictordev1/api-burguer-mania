using api_burguer_mania.Models;
using Microsoft.EntityFrameworkCore;
using api_burguer_mania.Context;
using Microsoft.AspNetCore.Mvc;
using api_burguer_mania.DTO;

namespace api_burguer_mania.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase {
    private readonly BurguerManiaDbContext _context;
    public ProductsController(BurguerManiaDbContext context) {
        _context = context;
    }
    [HttpGet("all")]
    public async Task<ActionResult<List<Product>>> GetAllProducts() {
        var products = await _context.Products.ToListAsync();
        if (products is null) {
            return NotFound(new {message = "There isn't any products."});
        }
        return Ok(products);
    }
    [HttpGet("GetProductById/{id:int}")]
    public async Task<ActionResult<Product>> GetProductById(int id) {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product is null) {
            return NotFound(new {message = "There isn't any product with the provided id."});
        }
        return Ok(product);
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateProduct(ProductDTO product) {
        if (product is null) {
            return BadRequest(new {message = "No product has been provided."});
        }
        var categoryId = await _context.Categories.FindAsync(product.CategoryId);
        if (categoryId is null) {
            return BadRequest(new {message = "The category id doesn't exist, please provide an existent category id."});
        }
        var newProduct = new Product {
            Name = product.Name,
            PathImage = product.PathImage,
            Price = product.Price,
            BaseDescription = product.BaseDescription,
            FullDescription = product.FullDescription,
            CategoryId = product.CategoryId
        };
        _context.Products.Add(newProduct);
        try {
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductById), new {id = newProduct.Id}, newProduct);
        } catch (DbUpdateException) {
            return StatusCode(500, new {message = "Internal error while trying to create the product."});
        }
    }
    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDTO newProduct) {
        var product = await _context.Products.FindAsync(id);
        if (product is null) {
            return NotFound(new {message = "Couldn't find a product with the provided id."});
        }
        product.Name = newProduct.Name;
        product.Price = newProduct.Price;
        product.BaseDescription = newProduct.BaseDescription;
        product.FullDescription = newProduct.FullDescription;
        product.CategoryId = newProduct.CategoryId;
        product.PathImage = newProduct.PathImage;
        try {
            await _context.SaveChangesAsync();
            return Ok(new {message = "Product updated with success."});
        } catch (DbUpdateException) {
            return StatusCode(500, new {message = "Internal error while trying to update the product."});
        }
    }
    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id) {
        var product = await _context.Products.FindAsync(id);
        if (product is null) {
            return NotFound(new {message = "Couldn't find the product with the provided id."});
        }
        _context.Products.Remove(product);
        try {
            await _context.SaveChangesAsync();
            return Ok(new {message = "Product removed with success."});
        } catch (DbUpdateException) {
            return StatusCode(500,  new {message = "Internal error while trying to delete the product."});
        }
    }
}