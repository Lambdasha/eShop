// Order.Application/Services/ICustomerService.cs
namespace Order.Application.Services;

using Order.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICustomerService
{
    Task<CustomerDto>             CreateCustomerAsync(CreateCustomerDto dto);
    Task<IEnumerable<UserAddressDto>> GetCustomerAddressesAsync(int customerId);
    Task<UserAddressDto>          SaveCustomerAddressAsync(UserAddressDto dto);
}