using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MvcApi.Data;
using MvcApi.Dto;
using MvcApi.Models;
using MvcApi.Services.Interfaces;

namespace MvcApi.Services;

public class VariantService : IVariantService
{
    private readonly DataContext _context;

    public VariantService(DataContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto> AddAsync(ProductVariant newProductVariant)
    {
        await _context.ProductVariants.AddAsync(newProductVariant);
        await _context.SaveChangesAsync();

        return new ResponseDto
        {
            Success = true, Message = $"ProductVariant {newProductVariant.Id} added.", Data = newProductVariant,
            StatusCode = 201
        };
    }

    public IQueryable<ProductVariant> GetAll()
    {
        return _context.ProductVariants.AsQueryable();
    }

    public async Task<ProductVariant?> GetOneAsync(Expression<Func<ProductVariant, bool>> filter)
    {
        return await _context.ProductVariants.FirstOrDefaultAsync(filter);
    }

    public async Task<ResponseDto> UpdateAsync(int id, ProductVariantUpdataDto updatedProductVariant)
    {
        var productVariant = await GetOneAsync(pv => pv.Id == id);

        if (productVariant == null)
        {
            return new ResponseDto
            {
                Success = false, Message = $"ProductVariant {id} does not exist.", Data = { },
                StatusCode = 404
            };
        }

        // updatedUser.UserName ??= user.UserName;
        productVariant.UpdatedAt = DateTime.UtcNow;

        _context.Entry(productVariant).CurrentValues.SetValues(updatedProductVariant);

        await _context.SaveChangesAsync();

        return new ResponseDto
        {
            Success = true,
            Message = $"ProductVariant {productVariant.Id} updated.",
            Data = productVariant,
            StatusCode = 200
        };
    }

    public async Task<ResponseDto> RemoveAsync(int id)
    {
        var productVariant = await GetOneAsync(pv => pv.Id == id);

        if (productVariant == null)
        {
            return new ResponseDto
            {
                Success = false, Message = $"ProductVariant {id} does not exist.", Data = { }, StatusCode = 404
            };
        }

        _context.ProductVariants.Remove(productVariant);
        await _context.SaveChangesAsync();

        return new ResponseDto
        {
            Success = true, Message = $"ProductVariant {productVariant.Id} deleted successfully", Data = productVariant,
            StatusCode = 200
        };
    }
}