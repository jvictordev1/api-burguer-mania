using api_burguer_mania.Models;
using Microsoft.EntityFrameworkCore;
using api_burguer_mania.Context;
using Microsoft.AspNetCore.Mvc;
using api_burguer_mania.DTO;

namespace api_burguer_mania.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase {
    private readonly BurguerManiaDbContext _context;

    public UsersController(BurguerManiaDbContext context) {
        _context = context;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<User>>> GetAllUsers() {
        var users = await _context.Users.ToListAsync();
        if (users is null || !users.Any()) {
            return NotFound(new { message = "There are no users." });
        }
        return Ok(users);
    }

    [HttpGet("GetUserById/{id:int}")]
    public async Task<ActionResult<User>> GetUserById(int id) {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user is null) {
            return NotFound(new { message = "No user found with the provided id." });
        }
        return Ok(user);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser(UserDTO userDto) {
        if (userDto is null) {
            return BadRequest(new { message = "No user data provided." });
        }

        var newUser = new User {
            Name = userDto.Name,
            Email = userDto.Email,
            Password = userDto.Password
        };

        _context.Users.Add(newUser);
        try {
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        } catch (DbUpdateException) {
            return StatusCode(500, new { message = "Internal error while trying to create the user." });
        }
    }

    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> UpdateUser(int id, UserDTO updatedUserDto) {
        var user = await _context.Users.FindAsync(id);
        if (user is null) {
            return NotFound(new { message = "User with the provided id not found." });
        }

        user.Name = updatedUserDto.Name;
        user.Email = updatedUserDto.Email;
        user.Password = updatedUserDto.Password;

        try {
            await _context.SaveChangesAsync();
            return Ok(new { message = "User updated successfully." });
        } catch (DbUpdateException) {
            return StatusCode(500, new { message = "Internal error while trying to update the user." });
        }
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteUser(int id) {
        var user = await _context.Users.FindAsync(id);
        if (user is null) {
            return NotFound(new { message = "User with the provided id not found." });
        }

        _context.Users.Remove(user);
        try {
            await _context.SaveChangesAsync();
            return Ok(new { message = "User deleted successfully." });
        } catch (DbUpdateException) {
            return StatusCode(500, new { message = "Internal error while trying to delete the user." });
        }
    }
}
