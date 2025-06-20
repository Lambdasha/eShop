// Order.API/Controllers/CustomerController.cs
namespace Order.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Order.Application.Models;
using Order.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _svc;
    public CustomerController(ICustomerService svc) => _svc = svc;

    [HttpPost("CreateCustomer")]
    public async Task<ActionResult<CustomerDto>> CreateCustomer([FromBody] CreateCustomerDto dto)
    {
        var created = await _svc.CreateCustomerAsync(dto);
        return CreatedAtAction(nameof(CreateCustomer), new { id = created.Id }, created);
    }

    [HttpGet("GetCustomerAddressByUserId/{customerId}")]
    public async Task<ActionResult<IEnumerable<UserAddressDto>>> GetCustomerAddressByUserId(int customerId)
        => Ok(await _svc.GetCustomerAddressesAsync(customerId));

    [HttpPost("SaveCustomerAddress")]
    public async Task<ActionResult<UserAddressDto>> SaveCustomerAddress([FromBody] UserAddressDto dto)
        => Ok(await _svc.SaveCustomerAddressAsync(dto));
}