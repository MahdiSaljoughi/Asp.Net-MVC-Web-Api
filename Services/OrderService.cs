using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MvcApi.Data;
using MvcApi.Dto;
using MvcApi.Models;
using MvcApi.Services.Interfaces;

namespace MvcApi.Services;

public class OrderService : IOrderService
{
    private readonly DataContext _context;

    public OrderService(DataContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto> AddAsync(Order newOrder)
    {
        await _context.Orders.AddAsync(newOrder);
        await _context.SaveChangesAsync();

        return new ResponseDto
            { Success = true, Message = $"Order {newOrder.Id} added.", Data = newOrder, StatusCode = 201 };
    }

    public IQueryable<Order> GetAll()
    {
        return _context.Orders.AsQueryable();
    }

    public async Task<ResponseDto> GetCurrentOrder()
    {
        // var userIdClaim = _httpContextAccessor.HttpContext?.User.Claims
        //     .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //
        // if (userIdClaim == null)
        // {
        //     return new ResponseDto
        //     {
        //         Success = false, Message = "User ID not found in token", Data = { }, StatusCode = 404
        //     };
        // }
        //
        // var userId = Guid.Parse(userIdClaim.Value);
        // var user = await GetOneAsync(u => u.Id == userId);
        //
        // if (user == null)
        // {
        //     return new ResponseDto
        //     {
        //         Success = false, Message = "User not found", Data = { }, StatusCode = 404
        //     };
        // }
        //
        // return new ResponseDto
        // {
        //     Success = true, Message = "User found", Data = user, StatusCode = 200
        // };
        return new ResponseDto
        {
            Success = true, Message = "User found", Data = { }, StatusCode = 200
        };
    }

    public async Task<Order?> GetOneAsync(Expression<Func<Order, bool>> filter)
    {
        return await _context.Orders.FirstOrDefaultAsync(filter);
    }

    public async Task<ResponseDto> UpdateAsync(int id, OrderUpdataDto updatedOrder)
    {
        var order = await GetOneAsync(o => o.Id == id);

        if (order == null)
        {
            return new ResponseDto
            {
                Success = false, Message = $"Order {id} does not exist.", Data = { },
                StatusCode = 404
            };
        }

        // updatedUser.UserName ??= user.UserName;
        order.UpdatedAt = DateTime.UtcNow;

        _context.Entry(order).CurrentValues.SetValues(updatedOrder);
        await _context.SaveChangesAsync();

        return new ResponseDto
        {
            Success = true,
            Message = $"User {order.Id} updated.",
            Data = order,
            StatusCode = 200
        };
    }

    public async Task<ResponseDto> RemoveAsync(int id)
    {
        var order = await GetOneAsync(o => o.Id == id);

        if (order == null)
        {
            return new ResponseDto
            {
                Success = false, Message = $"Order {id} does not exist.", Data = { }, StatusCode = 404
            };
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return new ResponseDto
        {
            Success = true, Message = $"Order {order.Id} deleted successfully", Data = order, StatusCode = 200
        };
    }
}