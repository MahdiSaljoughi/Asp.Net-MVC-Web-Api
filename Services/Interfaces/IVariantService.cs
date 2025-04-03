using System.Linq.Expressions;
using MvcApi.Dto;
using MvcApi.Models;

namespace MvcApi.Services.Interfaces;

public interface IVariantService
{
    public Task<ResponseDto> AddAsync(ProductVariant variant);
    
    public IQueryable<ProductVariant> GetAll();
    
    public Task<ProductVariant?> GetOneAsync(Expression<Func<ProductVariant, bool>> filter);
    
    public Task<ResponseDto> UpdateAsync(int id, ProductVariantUpdataDto updatedProductVariant);
    
    public Task<ResponseDto> RemoveAsync(int id);
}