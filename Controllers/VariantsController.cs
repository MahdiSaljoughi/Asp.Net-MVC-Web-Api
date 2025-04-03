using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcApi.Dto;
using MvcApi.Models;
using MvcApi.Models.Enums;
using MvcApi.Services.Interfaces;

namespace MvcApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class VariantsController : ControllerBase
{
    private readonly IVariantService _variantService;

    public VariantsController(IVariantService variantService)
    {
        _variantService = variantService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductVariant productVariant)
    {
        var result = await _variantService.AddAsync(productVariant);

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_variantService.GetAll());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var user = await _variantService.GetOneAsync(u => u.Id == id);

        if (user == null)
            return NotFound(new { message = $"ProductVariant {id} not found" });

        return Ok(user);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Update(int id, ProductVariantUpdataDto updatedProductVariant)
    {
        var result = await _variantService.UpdateAsync(id, updatedProductVariant);

        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _variantService.RemoveAsync(id);

        return StatusCode(result.StatusCode, result);
    }
}