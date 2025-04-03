using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MvcApi.Data;
using MvcApi.Dto;
using MvcApi.Models;
using MvcApi.Services.Interfaces;

namespace MvcApi.Services;

public class ProductService : IProductService
{
    private readonly DataContext _context;

    public ProductService(DataContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto> AddAsync(Product newProduct)
    {
        var existingProduct = await GetOneAsync(p => p.Slug == newProduct.Slug);

        if (existingProduct != null)
        {
            return new ResponseDto
            {
                Success = false, Message = $"Product {newProduct.Title} already exists.", Data = { }, StatusCode = 400
            };
        }

        await _context.Products.AddAsync(newProduct);
        await _context.SaveChangesAsync();

        return new ResponseDto
            { Success = true, Message = $"Product {newProduct.Title} added.", Data = newProduct, StatusCode = 201 };
    }

    public IQueryable<Product> GetAll()
    {
        return _context.Products.AsQueryable();
    }

    public async Task<Product?> GetOneAsync(Expression<Func<Product, bool>> filter)
    {
        return await _context.Products.FirstOrDefaultAsync(filter);
    }

    public async Task<ResponseDto> UpdateAsync(int id, ProductUpdataDto updatedProduct)
    {
        var product = await GetOneAsync(p => p.Id == id);

        if (product == null)
        {
            return new ResponseDto
            {
                Success = false, Message = $"Product {id} does not exist.", Data = { }, StatusCode = 404
            };
        }

        // updatedProduct.Title ??= product.Title;
        product.UpdatedAt = DateTime.UtcNow;

        _context.Entry(product).CurrentValues.SetValues(updatedProduct);
        await _context.SaveChangesAsync();

        return new ResponseDto
        {
            Success = true,
            Message = $"Product {product.Id} updated.",
            Data = product,
            StatusCode = 200
        };
    }

    public async Task<ResponseDto> RemoveAsync(int id)
    {
        var product = await GetOneAsync(p => p.Id == id);

        if (product == null)
        {
            return new ResponseDto
            {
                Success = false, Message = $"Product {id} does not exist.", Data = { }, StatusCode = 404
            };
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return new ResponseDto
        {
            Success = true, Message = $"Product {product.Id} deleted successfully", Data = product, StatusCode = 200
        };
    }
}