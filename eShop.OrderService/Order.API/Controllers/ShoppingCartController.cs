// Order.API/Controllers/ShoppingCartController.cs
namespace Order.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Order.Application.Models;
using Order.Application.Services;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartService _svc;
    public ShoppingCartController(IShoppingCartService svc) 
        => _svc = svc;

    /// <summary>
    /// GET /api/ShoppingCart/GetShoppingCartByCustomerId/{customerId}
    /// </summary>
    [HttpGet("GetShoppingCartByCustomerId/{customerId}")]
    public async Task<ActionResult<ShoppingCartDto?>> GetShoppingCartByCustomerId(int customerId)
    {
        var cart = await _svc.GetShoppingCartByCustomerIdAsync(customerId);
        if (cart is null)
            return NotFound();
        return Ok(cart);
    }

    /// <summary>
    /// POST /api/ShoppingCart/SaveShoppingCart
    /// </summary>
    [HttpPost("SaveShoppingCart")]
    public async Task<ActionResult<ShoppingCartDto>> SaveShoppingCart([FromBody] ShoppingCartDto dto)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);

        var saved = await _svc.SaveShoppingCartAsync(dto);
        return Ok(saved);
    }

    /// <summary>
    /// DELETE /api/ShoppingCart/DeleteShoppingCart/{customerId}
    /// </summary>
    [HttpDelete("DeleteShoppingCart/{customerId}")]
    public async Task<IActionResult> DeleteShoppingCart(int customerId)
    {
        await _svc.DeleteShoppingCartAsync(customerId);
        return NoContent();
    }
    
    /// <summary>
    /// DELETE /api/ShoppingCart/{customerId}/{itemId}
    /// </summary>
    [HttpDelete("{customerId}/{itemId}")]
    public async Task<IActionResult> DeleteCartItem(int customerId, int itemId)
    {
        var deleted = await _svc.DeleteShoppingCartItemAsync(customerId, itemId);
        return deleted ? NoContent() : NotFound();
    }
}