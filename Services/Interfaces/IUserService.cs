using MvcApi.Models;

namespace MvcApi.Services.Interfaces;

public interface IUserService
{
    public Task Add(User user);
    
    public IQueryable<User> GetAll();
    
    public Task<User?> GetOne(Guid id);
    
    public Task Update(User user, User updatedUser);
    
    public Task UpdateRange(User[] users);
    
    public Task Remove(User user);
    
    public Task RemoveRange(User[] users);
}