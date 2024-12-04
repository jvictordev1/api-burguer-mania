using api_burguer_mania.Models;
using Microsoft.EntityFrameworkCore;
using api_burguer_mania.Context;
using Microsoft.AspNetCore.Mvc;
using api_burguer_mania.DTO;

namespace api_burguer_mania.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase {
    private readonly BurguerManiaDbContext _context;

    public OrdersController(BurguerManiaDbContext context) {
        _context = context;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<Order>>> GetAllOrders() {
        var orders = await _context.Orders.Include(o => o.Status).ToListAsync();
        if (orders is null || !orders.Any()) {
            return NotFound(new { message = "There are no orders." });
        }
        return Ok(orders);
    }

    [HttpGet("GetOrderById/{id:int}")]
    public async Task<ActionResult<Order>> GetOrderById(int id) {
        var order = await _context.Orders.Include(o => o.Status).FirstOrDefaultAsync(o => o.Id == id);
        if (order is null) {
            return NotFound(new { message = "No order found with the provided id." });
        }
        return Ok(order);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder(OrderDTO orderDto) {
        if (orderDto is null) {
            return BadRequest(new { message = "No order data provided." });
        }

        var status = await _context.Status.FindAsync(orderDto.StatusId);
        if (status is null) {
            return BadRequest(new { message = "The status id doesn't exist, please provide a valid status id." });
        }

        var user = await _context.Users.FindAsync(orderDto.UserId);
        if (user is null) {
            return BadRequest(new { message = "The user doesn't exist, please provide a existent user." });
        }

        var newOrder = new Order {
            Value = orderDto.Value,
            StatusId = orderDto.StatusId,
            Description = orderDto.Description
        };
        _context.Orders.Add(newOrder);
        try {
            await _context.SaveChangesAsync();
        } catch (DbUpdateException) {
            return StatusCode(500, new { message = "Internal error while trying to create the order." });
        }
        var newOrderUser = new OrderUser {
            OrderId = newOrder.Id,
            UserId = user.Id
        };
        _context.OrdersUsers.Add(newOrderUser);
        foreach (var product in orderDto.Products) {
            var productExists = await _context.Products.AnyAsync(p => p.Id == product.Id);
            if (productExists) {
                var newOrderProduct = new OrderProduct {
                    Amount = product.Amount,
                    OrderId = newOrder.Id,
                    ProductId = product.Id
                };
                _context.OrdersProducts.Add(newOrderProduct);
            } else {
                return BadRequest(new {message = "The provided product doesn't exist."});
            }
        }
        try {
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.Id }, newOrder);
        } catch (DbUpdateException) {
            return StatusCode(500, new { message = "Internal error while trying to create the order." });
        }
    }

    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> UpdateOrder(int id, OrderDTO updatedOrderDto) {
        var order = await _context.Orders.FindAsync(id);
        if (order is null) {
            return NotFound(new { message = "Order with the provided id not found." });
        }

        var status = await _context.Status.FindAsync(updatedOrderDto.StatusId);
        if (status is null) {
            return BadRequest(new { message = "The status id doesn't exist, please provide a valid status id." });
        }

        order.Value = updatedOrderDto.Value;
        order.StatusId = updatedOrderDto.StatusId;
        order.Description = updatedOrderDto.Description;

        try {
            await _context.SaveChangesAsync();
            return Ok(new { message = "Order updated successfully." });
        } catch (DbUpdateException) {
            return StatusCode(500, new { message = "Internal error while trying to update the order." });
        }
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteOrder(int id) {
        var order = await _context.Orders.FindAsync(id);
        if (order is null) {
            return NotFound(new { message = "Order with the provided id not found." });
        }

        _context.Orders.Remove(order);
        try {
            await _context.SaveChangesAsync();
            return Ok(new { message = "Order deleted successfully." });
        } catch (DbUpdateException) {
            return StatusCode(500, new { message = "Internal error while trying to delete the order." });
        }
    }
}
