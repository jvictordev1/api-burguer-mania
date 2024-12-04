using api_burguer_mania.Context;
using api_burguer_mania.Models;
using api_burguer_mania.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace api_burguer_mania.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase {
    private readonly BurguerManiaDbContext _context;
    public CategoryController(BurguerManiaDbContext context) {
        _context = context;
    }
    [HttpGet("all")]
    public async Task<ActionResult<List<Category>>> GetAllCategories() {
        var categories = await _context.Categories.ToListAsync();
        if (categories is null || !categories.Any()) {
            return NotFound(new {message = "There isn't any categories created."});
        }
        return Ok(categories);
    }
    [HttpGet("GetCategoryById/{id:int}")]
    public async Task<ActionResult<Category>> GetCategoryById(int id) {
        var category = await _context.Categories.FindAsync(id);
        if (category is null) {
            return NotFound("Couldn't find a category with the provided id.");
        }
        return Ok(category);
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateCategory(CategoryDTO newCategory) {
        if (newCategory is null) {
            return BadRequest("No category was provided.");
        }
        var category = new Category {
            Description = newCategory.Description,
            Name = newCategory.Name,
            PathImage = newCategory.PathImage
        };
        try {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategoryById), new {id = category.Id}, category);
        } catch (DbUpdateException) {
            return StatusCode(500, "Internal error while trying to create the category.");
        }
    }
    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> UpdateCategory(int id, CategoryDTO newCategory) {
        var category = await _context.Categories.FindAsync(id);
        if (category is null) {
            return NotFound(new {message = "Couldn't find a category with the respective id."});
        }
        if (await _context.Categories.AnyAsync(c => c.Name == newCategory.Name && c.Id != id)) {
            return BadRequest(new {message = "There's already a category with the provided name."});
        }
        category.Description = newCategory.Description;
        category.Name = newCategory.Name;
        category.PathImage = newCategory.PathImage;
        try {
            await _context.SaveChangesAsync();
            return Ok(new {message = "Category updated with success."});
        } catch (DbUpdateException) {
            return StatusCode(400, new {message = "Internal error while trying to update the category."});
        }
    }
    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id) {
        var category = await _context.Categories.FindAsync(id);
        if (category is null) {
            return NotFound(new {message = "Couldn't find a category with the provided id."});
        }
        _context.Categories.Remove(category);
        try {
            await _context.SaveChangesAsync();
            return Ok(new {message = "Category deleted with success."});
        } catch (DbUpdateException) {
            return StatusCode(500, new {message = "Internal error while trying to delete the category."});
        }
    }
}