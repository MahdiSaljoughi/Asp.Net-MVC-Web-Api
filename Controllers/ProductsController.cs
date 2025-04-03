using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcApi.Dto;
using MvcApi.Models;
using MvcApi.Models.Enums;
using MvcApi.Services.Interfaces;

namespace MvcApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> Create(Product product)
    {
        var result = await _productService.AddAsync(product);

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_productService.GetAll());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var product = await _productService.GetOneAsync(p => p.Id == id);

        if (product == null)
            return NotFound(new { message = $"Product {id} not found" });

        return Ok(product);
    }

    [HttpPatch("{id:int}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> Update(int id, ProductUpdataDto updatedProduct)
    {
        var result = await _productService.UpdateAsync(id, updatedProduct);

        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productService.RemoveAsync(id);

        return StatusCode(result.StatusCode, result);
    }
}