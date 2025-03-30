using Microsoft.AspNetCore.Mvc;
using MvcApi.Models;
using MvcApi.Services;
using MvcApi.Services.Interfaces;

namespace MvcApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        await _userService.Add(user);
        return StatusCode(201, new { message = "User Created Successfully", user });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_userService.GetAll());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOne(Guid id)
    {
        var user = await _userService.GetOne(id);

        if (user == null)
            return NotFound(new { message = $"User {id} not found" });

        return Ok(user);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, User updatedUser)
    {
        var user = await _userService.GetOne(id);

        if (user == null)
            return NotFound(new { message = $"User {id} not found" });

        await _userService.Update(user!, updatedUser);

        return Ok(new { message = $"User {id} successfully updated", user });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var user = await _userService.GetOne(id);

        if (user == null)
            return NotFound(new { message = $"User {id} not found" });

        await _userService.Remove(user);

        return Ok(new { message = $"User {id} successfully deleted", user });
    }
}