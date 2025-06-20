// Order.Application/Services/IShoppingCartService.cs
namespace Order.Application.Services;

using Order.Application.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

public interface IShoppingCartService
{
    Task<ShoppingCartDto?>           GetShoppingCartByCustomerIdAsync(int customerId);
    Task<ShoppingCartDto>            SaveShoppingCartAsync(ShoppingCartDto dto);
    Task<bool>                             DeleteShoppingCartAsync(int customerId);
    Task<bool> DeleteShoppingCartItemAsync(int customerId, int itemId);
}