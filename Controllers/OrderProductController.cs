using api_burguer_mania.Models;
using Microsoft.EntityFrameworkCore;
using api_burguer_mania.Context;
using Microsoft.AspNetCore.Mvc;
using api_burguer_mania.DTO;

namespace api_burguer_mania.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderProductController : ControllerBase {
    private readonly BurguerManiaDbContext _context;

    public OrderProductController(BurguerManiaDbContext context) {
        _context = context;
    }

    [HttpPost("addProductToOrder")]
    public async Task<IActionResult> AddProductToOrder(OrderProductDTO orderProductDto) {
        if (orderProductDto is null) {
            return BadRequest(new { message = "No order product data provided." });
        }

        var order = await _context.Orders.FindAsync(orderProductDto.OrderId);
        if (order is null) {
            return NotFound(new { message = "Order not found." });
        }

        var product = await _context.Products.FindAsync(orderProductDto.ProductId);
        if (product is null) {
            return NotFound(new { message = "Product not found." });
        }

        var orderProduct = new OrderProduct {
            OrderId = orderProductDto.OrderId,
            ProductId = orderProductDto.ProductId
        };

        _context.OrdersProducts.Add(orderProduct);
        try {
            await _context.SaveChangesAsync();
            return Ok(new { message = "Product added to the order successfully." });
        } catch (DbUpdateException) {
            return StatusCode(500, new { message = "Internal error while trying to add the product to the order." });
        }
    }

    [HttpDelete("removeProductFromOrder/{orderId:int}/{productId:int}")]
    public async Task<IActionResult> RemoveProductFromOrder(int orderId, int productId) {
        var orderProduct = await _context.OrdersProducts
                                          .FirstOrDefaultAsync(op => op.OrderId == orderId && op.ProductId == productId);
        if (orderProduct is null) {
            return NotFound(new { message = "Product is not associated with this order." });
        }

        _context.OrdersProducts.Remove(orderProduct);
        try {
            await _context.SaveChangesAsync();
            return Ok(new { message = "Product removed from the order successfully." });
        } catch (DbUpdateException) {
            return StatusCode(500, new { message = "Internal error while trying to remove the product from the order." });
        }
    }
}
