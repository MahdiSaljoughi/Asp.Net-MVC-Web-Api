using Microsoft.AspNetCore.Mvc;
using MvcApi.Data;
using MvcApi.Interfaces;
using MvcApi.Models;

namespace MvcApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase, IUser
{
    private readonly DataContext _dbContext;

    public UsersController(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return StatusCode(201, new { message = "User Created Successfully", user });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return StatusCode(200, _dbContext.Users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(Guid id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user == null)
            return NotFound(new { message = $"User {id} not found" });

        return Ok(user);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] User updatedUser)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user == null)
            return NotFound(new { message = $"User {id} not found" });

        updatedUser.Id = user.Id;
        updatedUser.CreatedAt = user.CreatedAt;
        updatedUser.UpdatedAt = DateTime.UtcNow;

        _dbContext.Entry(user).CurrentValues.SetValues(updatedUser);

        await _dbContext.SaveChangesAsync();
        return StatusCode(200, new { message = $"User {user.Id} successfully updated", user });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _dbContext.Users.FindAsync(id);
        if (user == null)
            return NotFound(new { message = $"User {id} not found" });

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

        return StatusCode(200, new { message = $"User {user.Id} successfully deleted", user });
    }
}