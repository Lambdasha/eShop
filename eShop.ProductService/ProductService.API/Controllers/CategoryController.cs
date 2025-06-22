// ProductService.API/Controllers/CategoryController.cs
namespace ProductService.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Models;
using ProductService.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _svc;
    public CategoryController(ICategoryService svc) => _svc = svc;

    // POST /api/Category/SaveCategory
    [HttpPost("SaveCategory")]
    public async Task<ActionResult<CategoryDto>> SaveCategory([FromBody] CategoryDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var saved = await _svc.SaveAsync(dto);
        return Ok(saved);
    }

    // GET /api/Category/GetAllCategory
    [HttpGet("GetAllCategory")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategory()
        => Ok(await _svc.GetAllAsync());

    // GET /api/Category/GetCategoryById/{id}
    [HttpGet("GetCategoryById/{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
    {
        var dto = await _svc.GetByIdAsync(id);
        return dto == null ? NotFound() : Ok(dto);
    }

    // GET /api/Category/GetCategoryByParentCategoryId/{parentId}
    [HttpGet("GetCategoryByParentCategoryId/{parentId}")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetByParent(int parentId)
        => Ok(await _svc.GetByParentIdAsync(parentId));
    
    // DELETE /api/Category/Delete/{id}
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
        => await _svc.DeleteAsync(id) ? NoContent() : NotFound();
}