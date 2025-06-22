// ProductService.API/Controllers/ProductController.cs
namespace ProductService.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Models;
using ProductService.Application.Services;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _svc;
    public ProductController(IProductService svc) => _svc = svc;

    /// <summary>
    /// GET /api/Product/GetListProducts?page=1&size=10[&categoryId=5]
    /// </summary>
    [HttpGet("GetListProducts")]
    public async Task<ActionResult<PaginatedResult<ProductDto>>> GetListProducts(
        [FromQuery] int page       = 1,
        [FromQuery] int size       = 10)
    {
        var result = await _svc.GetProductsAsync(page, size);
        return Ok(result);
    }
    
    
    [HttpGet("GetListProductsByCategory")]
    public async Task<ActionResult<PaginatedResult<ProductDto>>> GetListProductsByCategory(
        [FromQuery] int page       = 1,
        [FromQuery] int size       = 10,
        [FromQuery] int category   = default)
    {
        var result = await _svc.GetProductsAsync(page, size, category);
        return Ok(result);
    }

    /// <summary>
    /// GET /api/Product/GetProductById/{id}
    /// </summary>
    [HttpGet("GetProductById/{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById(int id)
    {
        var dto = await _svc.GetByIdAsync(id);
        return dto == null ? NotFound() : Ok(dto);
    }

    /// <summary>
    /// POST /api/Product/Save
    /// </summary>
    [HttpPost("Save")]
    public async Task<ActionResult<ProductDto>> Save([FromBody] CreateProductDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _svc.CreateAsync(dto);
        return CreatedAtAction(
            nameof(GetProductById),
            new { id = created.Id },
            created);
    }

    /// <summary>
    /// PUT /api/Product/Update/{id}
    /// </summary>
    [HttpPut("Update/{id}")]
    public async Task<ActionResult<ProductDto>> Update(
        int id,
        [FromBody] UpdateProductDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var updated = await _svc.UpdateAsync(id, dto);
        return updated == null ? NotFound() : Ok(updated);
    }

    /// <summary>
    /// PUT /api/Product/InActive/{id}
    /// </summary>
    [HttpPut("InActive/{id}")]
    public async Task<IActionResult> InActive(int id)
    {
        var ok = await _svc.InactivateAsync(id);
        return ok ? NoContent() : NotFound();
    }

    /// <summary>
    /// GET /api/Product/GetProductByName/{name}
    /// </summary>
    [HttpGet("GetProductByName/{name}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetByName(string name)
    {
        var list = await _svc.GetByNameAsync(name);
        return Ok(list);
    }

    /// <summary>
    /// DELETE /api/Product/DeleteProduct/{id}
    /// </summary>
    [HttpDelete("DeleteProduct/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var deleted = await _svc.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
