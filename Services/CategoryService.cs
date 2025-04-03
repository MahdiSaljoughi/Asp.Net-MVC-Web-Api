using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MvcApi.Data;
using MvcApi.Dto;
using MvcApi.Models;
using MvcApi.Services.Interfaces;

namespace MvcApi.Services;

public class CategoryService : ICategoryService
{
    private readonly DataContext _context;

    public CategoryService(DataContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto> AddAsync(Category newCategory)
    {
        var existingCategory = await GetOneAsync(c => c.Title == newCategory.Title);

        if (existingCategory != null)
        {
            return new ResponseDto
            {
                Success = false, Message = $"Category {newCategory.Title} already exists.", Data = { }, StatusCode = 400
            };
        }

        await _context.Categories.AddAsync(newCategory);
        await _context.SaveChangesAsync();

        return new ResponseDto
            { Success = true, Message = $"Category {newCategory.Title} added.", Data = newCategory, StatusCode = 201 };
    }

    public IQueryable<Category> GetAll()
    {
        return _context.Categories.AsQueryable();
    }

    public async Task<Category?> GetOneAsync(Expression<Func<Category, bool>> filter)
    {
        return await _context.Categories.FirstOrDefaultAsync(filter);
    }

    public async Task<ResponseDto> UpdateAsync(int id, CategoryUpdataDto updatedCategory)
    {
        var category = await GetOneAsync(c => c.Id == id);

        if (category == null)
        {
            return new ResponseDto
            {
                Success = false, Message = $"Category {id} does not exist.", Data = { },
                StatusCode = 404
            };
        }

        // updatedCategory.Title ??= category.Title;
        category.UpdatedAt = DateTime.UtcNow;

        _context.Entry(category).CurrentValues.SetValues(updatedCategory);
        await _context.SaveChangesAsync();

        return new ResponseDto
        {
            Success = true,
            Message = $"Category {category.Title} updated.",
            Data = category,
            StatusCode = 200
        };
    }

    public async Task<ResponseDto> RemoveAsync(int id)
    {
        var category = await GetOneAsync(c => c.Id == id);

        if (category == null)
        {
            return new ResponseDto
            {
                Success = false, Message = $"Category {id} does not exist.", Data = { }, StatusCode = 404
            };
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return new ResponseDto
        {
            Success = true, Message = $"Category {category.Title} deleted successfully", Data = category,
            StatusCode = 200
        };
    }
}