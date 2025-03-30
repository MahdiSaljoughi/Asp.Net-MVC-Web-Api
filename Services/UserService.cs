using MvcApi.Data;
using MvcApi.Models;
using MvcApi.Services.Interfaces;

namespace MvcApi.Services;

public class UserService : IUserService
{
    private readonly DBContext _context;

    public UserService(DBContext context)
    {
        _context = context;
    }

    public async Task Add(User user)
    {
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public IQueryable<User> GetAll()
    {
        return _context.Users.AsQueryable();
    }

    public async Task<User?> GetOne(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task Update(User user, User updatedUser)
    {
        updatedUser.Id = user.Id;
        updatedUser.CreatedAt = user.CreatedAt;
        updatedUser.UpdatedAt = DateTime.UtcNow;

        _context.Users.Entry(user).CurrentValues.SetValues(updatedUser);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateRange(User[] users)
    {
        _context.Users.UpdateRange(users);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveRange(User[] users)
    {
        _context.Users.RemoveRange(users);
        await _context.SaveChangesAsync();
    }
}