using Microsoft.AspNetCore.Mvc;
using Order.Application.Models;
using Order.Application.Services;

namespace Order.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _svc;
    public OrderController(IOrderService svc) => _svc = svc;

    [HttpGet("GetCustomerOrders/{customerId}")]
    public async Task<IActionResult> GetByCustomer(int customerId)
        => Ok(await _svc.GetOrdersByCustomerAsync(customerId));

    [HttpGet("GetAllOrders")]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
        => Ok(await _svc.GetAllOrdersAsync(page, size));

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var dto = await _svc.GetOrderAsync(id);
        return dto == null ? NotFound() : Ok(dto);
    }

    [HttpPost("CreateOrder")]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        var id = await _svc.CreateOrderAsync(dto);
        return CreatedAtAction(nameof(Get), new { id }, null);
    }

    [HttpPut("CancelOrder")]
    public async Task<IActionResult> Cancel(int id)
    {
        await _svc.CancelOrderAsync(id);
        return NoContent();
    }
}