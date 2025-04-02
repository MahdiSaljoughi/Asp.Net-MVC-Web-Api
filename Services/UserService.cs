using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MvcApi.Data;
using MvcApi.Dto;
using MvcApi.Models;
using MvcApi.Services.Interfaces;

namespace MvcApi.Services;

public class UserService : IUserService
{
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto> AddAsync(User newUser)
    {
        var existingUser = await GetOneAsync(user => user.Phone == newUser.Phone);

        if (existingUser != null)
        {
            return new ResponseDto
            {
                Success = false, Message = $"User {newUser.Phone} already exists.", Data = { }, StatusCode = 400
            };
        }

        newUser.CreatedAt = DateTime.UtcNow;
        newUser.UpdatedAt = DateTime.UtcNow;

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        return new ResponseDto
            { Success = true, Message = $"User {newUser.Phone} added.", Data = newUser, StatusCode = 201 };
    }

    public IQueryable<User> GetAll()
    {
        return _context.Users.AsQueryable();
    }

    public async Task<User?> GetOneAsync(Expression<Func<User, bool>> filter)
    {
        return await _context.Users.FirstOrDefaultAsync(filter);
    }

    public async Task<ResponseDto> UpdateAsync(User user, User updatedUser)
    {
        var existingUser = await GetOneAsync(user => user.Id == updatedUser.Id);

        if (existingUser == null)
        {
            return new ResponseDto
            {
                Success = false, Message = $"User {updatedUser.Id} does not exists.", Data = updatedUser,
                StatusCode = 400
            };
        }

        updatedUser.Id = user.Id;
        updatedUser.CreatedAt = user.CreatedAt;
        updatedUser.UpdatedAt = DateTime.UtcNow;

        _context.Users.Entry(user).CurrentValues.SetValues(updatedUser);

        await _context.SaveChangesAsync();

        return new ResponseDto
            { Success = true, Message = $"User {user.Id} updated.", Data = updatedUser, StatusCode = 200 };
    }

    public async Task UpdateRangeAsync(User[] users)
    {
        _context.Users.UpdateRange(users);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveRangeAsync(User[] users)
    {
        _context.Users.RemoveRange(users);
        await _context.SaveChangesAsync();
    }
}