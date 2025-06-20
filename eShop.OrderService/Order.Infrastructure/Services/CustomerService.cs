// Order.Infrastructure/Services/CustomerService.cs
namespace Order.Infrastructure.Services;

using Microsoft.EntityFrameworkCore;
using Order.Application.Models;
using Order.Application.Services;
using Order.Domain.Entities;
using Order.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repo;
    public CustomerService(ICustomerRepository repo) => _repo = repo;

    public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto)
    {
        var c = new Customer {
            FirstName  = dto.FirstName,
            LastName   = dto.LastName,
            Gender     = dto.Gender,
            Phone      = dto.Phone,
            ProfilePic = dto.ProfilePic,
            UserId     = dto.UserId
        };
        await _repo.AddCustomerAsync(c);
        return new CustomerDto {
            Id         = c.Id,
            FirstName  = c.FirstName,
            LastName   = c.LastName,
            Gender     = c.Gender,
            Phone      = c.Phone,
            ProfilePic = c.ProfilePic,
            UserId     = c.UserId
        };
    }

    public async Task<IEnumerable<UserAddressDto>> GetCustomerAddressesAsync(int customerId)
    {
        var list = await _repo.GetAddressesByCustomerIdAsync(customerId);
        return list.Select(ua => new UserAddressDto {
            Id          = ua.Id,
            CustomerId  = ua.CustomerId,
            AddressId   = ua.AddressId,
            Street1     = ua.Address.Street1,
            Street2     = ua.Address.Street2,
            City        = ua.Address.City,
            Zipcode     = ua.Address.Zipcode,
            State       = ua.Address.State,
            Country     = ua.Address.Country,
            IsDefault   = ua.IsDefaultAddress
        });
    }

    public async Task<UserAddressDto> SaveCustomerAddressAsync(UserAddressDto dto)
    {
        UserAddress ua = dto.Id == 0
            ? new UserAddress {
                  CustomerId       = dto.CustomerId,
                  IsDefaultAddress = dto.IsDefault,
                  Address          = new Address()
              }
            : await _repo.GetUserAddressByIdAsync(dto.Id)
                  ?? throw new KeyNotFoundException($"UserAddress {dto.Id} not found");

        ua.IsDefaultAddress = dto.IsDefault;
        ua.Address.Street1  = dto.Street1;
        ua.Address.Street2  = dto.Street2;
        ua.Address.City     = dto.City;
        ua.Address.Zipcode  = dto.Zipcode;
        ua.Address.State    = dto.State;
        ua.Address.Country  = dto.Country;

        if (dto.Id == 0)
            await _repo.AddUserAddressAsync(ua);
        else
            await _repo.UpdateUserAddressAsync(ua);

        return new UserAddressDto {
            Id          = ua.Id,
            CustomerId  = ua.CustomerId,
            AddressId   = ua.AddressId,
            Street1     = ua.Address.Street1,
            Street2     = ua.Address.Street2,
            City        = ua.Address.City,
            Zipcode     = ua.Address.Zipcode,
            State       = ua.Address.State,
            Country     = ua.Address.Country,
            IsDefault   = ua.IsDefaultAddress
        };
    }
}
