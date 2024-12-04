using api_burguer_mania.Models;
using Microsoft.EntityFrameworkCore;
using api_burguer_mania.Context;
using Microsoft.AspNetCore.Mvc;
using api_burguer_mania.DTO;

namespace api_burguer_mania.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderUserController : ControllerBase {
    private readonly BurguerManiaDbContext _context;

    public OrderUserController(BurguerManiaDbContext context) {
        _context = context;
    }

    [HttpPost("addUserToOrder")]
    public async Task<IActionResult> AddUserToOrder(OrderUserDTO orderUserDto) {
        if (orderUserDto is null) {
            return BadRequest(new { message = "No user order data provided." });
        }

        var order = await _context.Orders.FindAsync(orderUserDto.OrderId);
        if (order is null) {
            return NotFound(new { message = "Order not found." });
        }

        var user = await _context.Users.FindAsync(orderUserDto.UserId);
        if (user is null) {
            return NotFound(new { message = "User not found." });
        }

        var orderUser = new OrderUser {
            OrderId = orderUserDto.OrderId,
            UserId = orderUserDto.UserId
        };

        _context.OrdersUsers.Add(orderUser);
        try {
            await _context.SaveChangesAsync();
            return Ok(new { message = "User added to the order successfully." });
        } catch (DbUpdateException) {
            return StatusCode(500, new { message = "Internal error while trying to add the user to the order." });
        }
    }

    [HttpDelete("removeUserFromOrder/{orderId:int}/{userId:int}")]
    public async Task<IActionResult> RemoveUserFromOrder(int orderId, int userId) {
        var orderUser = await _context.OrdersUsers
                                      .FirstOrDefaultAsync(ou => ou.OrderId == orderId && ou.UserId == userId);
        if (orderUser is null) {
            return NotFound(new { message = "User is not associated with this order." });
        }

        _context.OrdersUsers.Remove(orderUser);
        try {
            await _context.SaveChangesAsync();
            return Ok(new { message = "User removed from the order successfully." });
        } catch (DbUpdateException) {
            return StatusCode(500, new { message = "Internal error while trying to remove the user from the order." });
        }
    }
}
