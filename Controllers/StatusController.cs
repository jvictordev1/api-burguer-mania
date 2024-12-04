using api_burguer_mania.Context;
using api_burguer_mania.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_burguer_mania.DTO;

namespace api_burguer_mania.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusController : ControllerBase {
    private readonly BurguerManiaDbContext _context;

    public StatusController(BurguerManiaDbContext context) {
        _context = context;
    }

    // GET: api/Status/all
    [HttpGet("all")]
    public async Task<ActionResult<List<Status>>> GetAllStatuses() {
        var statuses = await _context.Status.ToListAsync();
        if (statuses is null || statuses.Count == 0) {
            return NotFound(new { message = "No statuses found." });
        }
        return Ok(statuses);
    }

    // GET: api/Status/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Status>> GetStatusById(int id) {
        var status = await _context.Status.FindAsync(id);
        if (status is null) {
            return NotFound(new { message = "Status not found." });
        }
        return Ok(status);
    }

    // POST: api/Status/create
    [HttpPost("create")]
    public async Task<IActionResult> CreateStatus(StatusDTO newStatus) {
        if (newStatus is null || string.IsNullOrWhiteSpace(newStatus.Name)) {
            return BadRequest(new { message = "Invalid status data." });
        }
        var status = new Status {
            Name = newStatus.Name,
        };
        _context.Status.Add(status);
        try {
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStatusById), new { id = status.Id }, status);
        } catch (DbUpdateException) {
            return StatusCode(500, new { message = "An error occurred while creating the status." });
        }
    }

    // PUT: api/Status/update/{id}
    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> UpdateStatus(int id, StatusDTO updatedStatus) {
        var status = await _context.Status.FindAsync(id);
        if (status is null) {
            return NotFound(new { message = "Status not found." });
        }
        
        status.Name = updatedStatus.Name;

        try {
            await _context.SaveChangesAsync();
            return Ok(new { message = "Status updated successfully." });
        } catch (DbUpdateException) {
            return StatusCode(500, new { message = "An error occurred while updating the status." });
        }
    }
    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteStatus(int id) {
        var status = await _context.Status.FindAsync(id);
        if (status is null) {
            return NotFound(new { message = "Status not found." });
        }

        _context.Status.Remove(status);
        try {
            await _context.SaveChangesAsync();
            return Ok(new { message = "Status deleted successfully." });
        } catch (DbUpdateException) {
            return StatusCode(500, new { message = "An error occurred while deleting the status." });
        }
    }
}