using Microsoft.AspNetCore.Mvc;
using MvcApi.Data;
using MvcApi.Models;

namespace MvcApi.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly DataContext _dbContext;

    public UsersController(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _dbContext.Users.ToList();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = _dbContext.Users.Find(id);
        if (user == null)
            return NotFound(new { message = "User not found" });

        return Ok(user);
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }
    
    [HttpPatch("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
    {
        var user = _dbContext.Users.Find(id);
        if (user == null)
            return NotFound(new { message = "User not found" });

        updatedUser.Id = user.Id;

        _dbContext.Entry(user).CurrentValues.SetValues(updatedUser);
        user.UpdatedAt = DateTime.UtcNow;

        _dbContext.SaveChanges();
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = _dbContext.Users.Find(id);
        if (user == null)
            return NotFound(new { message = "User not found" });

        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();

        return NoContent();
    }
}