using Microsoft.AspNetCore.Mvc;
using Order.Application.Models;
using Order.Application.Services;

namespace Order.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartService _svc;
    public ShoppingCartController(IShoppingCartService svc) => _svc = svc;

    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetCart(int customerId)
        => Ok(await _svc.GetCartAsync(customerId));

    [HttpPost("customer/{customerId}/items")]
    public async Task<IActionResult> AddItem(int customerId, AddCartItemDto dto)
    {
        await _svc.AddItemAsync(customerId, dto);
        return NoContent();
    }

    [HttpDelete("{cartId}/items/{itemId}")]
    public async Task<IActionResult> RemoveItem(int cartId, int itemId)
    {
        await _svc.RemoveItemAsync(cartId, itemId);
        return NoContent();
    }

    [HttpPost("customer/{customerId}/checkout")]
    public async Task<IActionResult> Checkout(int customerId, CheckoutDto dto)
    {
        var orderId = await _svc.CheckoutAsync(customerId, dto);
        return CreatedAtAction(
            "Get", // returns to OrderController.Get
            "Order", new { id = orderId }, null);
    }
}