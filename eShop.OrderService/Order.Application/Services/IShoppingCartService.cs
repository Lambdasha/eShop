using Order.Application.Models;

namespace Order.Application.Services;

public interface IShoppingCartService
{
    Task<ShoppingCartDto> GetCartAsync(int customerId);
    Task AddItemAsync(int customerId, AddCartItemDto dto);
    Task RemoveItemAsync(int cartId, int itemId);
    Task<int> CheckoutAsync(int customerId, CheckoutDto dto);
}