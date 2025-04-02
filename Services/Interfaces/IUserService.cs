using System.Linq.Expressions;
using MvcApi.Dto;
using MvcApi.Models;

namespace MvcApi.Services.Interfaces;

public interface IUserService
{
    public Task<ResponseDto> AddAsync(User user);
    
    public IQueryable<User> GetAll();

    public Task<ResponseDto> GetCurrentUser();
    
    public Task<User?> GetOneAsync(Expression<Func<User, bool>> filter);
    
    public Task<ResponseDto> UpdateAsync(Guid id, UserUpdataDto updatedUser);
    
    public Task UpdateRangeAsync(User[] users);
    
    public Task<ResponseDto> RemoveAsync(Guid id);
    
    public Task RemoveRangeAsync(User[] users);
}