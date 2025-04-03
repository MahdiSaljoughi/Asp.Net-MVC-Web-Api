using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MvcApi.Data;
using MvcApi.Dto;
using MvcApi.Models;
using MvcApi.Services.Interfaces;

namespace MvcApi.Services;

public class UserService : IUserService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ResponseDto> AddAsync(User newUser)
    {
        var existingUser = await GetOneAsync(user => user.Phone == newUser.Phone);

        if (existingUser != null)
        {
            return new ResponseDto
                { Success = false, Message = $"User {newUser.Phone} already exists.", Data = { }, StatusCode = 400 };
        }

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        return new ResponseDto
            { Success = true, Message = $"User {newUser.Phone} added.", Data = newUser, StatusCode = 201 };
    }

    public IQueryable<User> GetAll()
    {
        return _context.Users.AsQueryable();
    }

    public async Task<ResponseDto> GetCurrentUser()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {
            return new ResponseDto
            {
                Success = false, Message = "User ID not found in token", Data = { }, StatusCode = 404
            };
        }

        var userId = Guid.Parse(userIdClaim.Value);
        var user = await GetOneAsync(u => u.Id == userId);

        if (user == null)
        {
            return new ResponseDto
            {
                Success = false, Message = "User not found", Data = { }, StatusCode = 404
            };
        }

        return new ResponseDto
        {
            Success = true, Message = "User found", Data = user, StatusCode = 200
        };
    }

    public async Task<User?> GetOneAsync(Expression<Func<User, bool>> filter)
    {
        return await _context.Users.FirstOrDefaultAsync(filter);
    }

    public async Task<ResponseDto> UpdateAsync(Guid id, UserUpdataDto updatedUser)
    {
        var user = await GetOneAsync(u => u.Id == id);

        if (user == null)
        {
            return new ResponseDto
            {
                Success = false, Message = $"User {id} does not exist.", Data = { },
                StatusCode = 404
            };
        }
        
        updatedUser.UserName ??= user.UserName;
        updatedUser.Email ??= user.Email;
        updatedUser.Address ??= user.Address;
        updatedUser.FirstName ??= user.FirstName;
        updatedUser.LastName ??= user.LastName;
        updatedUser.Role ??= user.Role;
        user.UpdatedAt = DateTime.UtcNow;

        _context.Entry(user).CurrentValues.SetValues(updatedUser);
        
        await _context.SaveChangesAsync();

        return new ResponseDto
        {
            Success = true,
            Message = $"User {user.Id} updated.",
            Data = user,
            StatusCode = 200
        };
    }

    public async Task UpdateRangeAsync(User[] users)
    {
        _context.Users.UpdateRange(users);
        await _context.SaveChangesAsync();
    }

    public async Task<ResponseDto> RemoveAsync(Guid id)
    {
        var user = await GetOneAsync(u => u.Id == id);

        if (user == null)
        {
            return new ResponseDto
            {
                Success = false, Message = $"User {id} does not exist.", Data = { }, StatusCode = 404
            };
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return new ResponseDto
        {
            Success = true, Message = $"User {user.Id} deleted successfully", Data = user, StatusCode = 200
        };
    }

    public async Task RemoveRangeAsync(User[] users)
    {
        _context.Users.RemoveRange(users);
        await _context.SaveChangesAsync();
    }
}