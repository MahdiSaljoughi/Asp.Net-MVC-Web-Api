using System.Linq.Expressions;
using MvcApi.Dto;
using MvcApi.Models;

namespace MvcApi.Services.Interfaces;

public interface IOrderService
{
    public Task<ResponseDto> AddAsync(Order order);
    
    public IQueryable<Order> GetAll();
    
    public Task<ResponseDto> GetCurrentOrder();
    
    public Task<Order?> GetOneAsync(Expression<Func<Order, bool>> filter);
    
    public Task<ResponseDto> UpdateAsync(int id, OrderUpdataDto updatedOrder);
    
    public Task<ResponseDto> RemoveAsync(int id);
}