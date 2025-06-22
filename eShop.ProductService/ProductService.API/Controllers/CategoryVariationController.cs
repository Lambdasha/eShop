// ProductService.API/Controllers/CategoryController.cs
namespace ProductService.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Models;
using ProductService.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CategoryVariationController : ControllerBase
{
    private readonly ICategoryVariationService _svc;
    public CategoryVariationController(ICategoryVariationService svc) => _svc = svc;

    // POST /api/Category/SaveCategoryVariation
    [HttpPost("SaveCategoryVariation")]
    public async Task<ActionResult<CategoryVariationDto>> SaveCategoryVariation([FromBody] CategoryVariationDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var saved = await _svc.SaveAsync(dto);
        return Ok(saved);
    }

    // GET /api/Category/GetAllCategoryVariation
    [HttpGet("GetAllCategoryVariation")]
    public async Task<ActionResult<IEnumerable<CategoryVariationDto>>> GetAllCategoryVariation()
        => Ok(await _svc.GetAllAsync());

    // GET /api/Category/GetCategoryVariationById/{id}
    [HttpGet("GetCategoryVariationById/{id}")]
    public async Task<ActionResult<CategoryVariationDto>> GetCategoryVariationById(int id)
    {
        var dto = await _svc.GetByIdAsync(id);
        return dto == null ? NotFound() : Ok(dto);
    }

    // GET /api/Category/GetCategoryVariationByCategoryId/{parentId}
    [HttpGet("GetCategoryVariationByCategoryId/{categoryId}")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoryVariationByCategoryId(int categoryId)
        => Ok(await _svc.GetByCategoryIdAsync(categoryId));
    
    // DELETE /api/CategoryVariation/Delete/{id}
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
        => await _svc.DeleteAsync(id) ? NoContent() : NotFound();
}