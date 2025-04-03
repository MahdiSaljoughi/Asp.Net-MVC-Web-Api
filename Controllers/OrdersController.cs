using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcApi.Dto;
using MvcApi.Models;
using MvcApi.Models.Enums;
using MvcApi.Services.Interfaces;

namespace MvcApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(Order order)
    {
        var result = await _orderService.AddAsync(order);

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public IActionResult GetAll()
    {
        return Ok(_orderService.GetAll());
    }

    // [HttpGet("me")]
    // [Authorize]
    // public async Task<IActionResult> GetCurrentOrder()
    // {
    //     var result = await _orderService.GetCurrentOrder();
    //
    //     return StatusCode(result.StatusCode, result);
    // }

    [HttpGet("{id:int}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> GetOne(int id)
    {
        var order = await _orderService.GetOneAsync(u => u.Id == id);

        if (order == null)
            return NotFound(new { message = $"Order {id} not found" });

        return Ok(order);
    }

    [HttpPatch("{id:int}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> Update(int id, OrderUpdataDto updatedOrder)
    {
        var result = await _orderService.UpdateAsync(id, updatedOrder);

        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _orderService.RemoveAsync(id);

        return StatusCode(result.StatusCode, result);
    }
}