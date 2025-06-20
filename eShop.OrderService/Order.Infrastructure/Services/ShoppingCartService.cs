// Order.Infrastructure/Services/ShoppingCartService.cs
namespace Order.Infrastructure.Services;

using Order.Application.Models;
using Order.Application.Services;
using Order.Domain.Entities;
using Order.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _repo;

    public ShoppingCartService(IShoppingCartRepository repo)
        => _repo = repo;

    public async Task<ShoppingCartDto?> GetShoppingCartByCustomerIdAsync(int customerId)
    {
        var cart = await _repo.GetByCustomerIdAsync(customerId);
        if (cart == null) return null;

        return new ShoppingCartDto
        {
            Id = cart.Id,
            CustomerId = cart.CustomerId,
            CustomerName = cart.CustomerName,
            Items = cart.Items.Select(i => new ShoppingCartItemDto
            {
                Id = i.Id,
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Qty = i.Qty,
                Price = i.Price
            }).ToList()
        };
    }

    public async Task<ShoppingCartDto> SaveShoppingCartAsync(ShoppingCartDto dto)
    {
        // Map DTO → entity
        var cart = await _repo.GetByCustomerIdAsync(dto.CustomerId)
                   ?? new ShoppingCart
                   {
                       CustomerId = dto.CustomerId,
                       CustomerName = dto.CustomerName
                   };

        // Replace items
        cart.Items.Clear();
        foreach (var item in dto.Items)
        {
            cart.Items.Add(new ShoppingCartItem
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Qty = item.Qty,
                Price = item.Price
            });
        }

        // Persist
        cart = dto.Id == 0
            ? await _repo.AddAsync(cart)
            : await _repo.UpdateAsync(cart);

        // Map back to DTO (with generated Id)
        return new ShoppingCartDto
        {
            Id = cart.Id,
            CustomerId = cart.CustomerId,
            CustomerName = cart.CustomerName,
            Items = cart.Items.Select(i => new ShoppingCartItemDto
            {
                Id = i.Id,
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Qty = i.Qty,
                Price = i.Price
            }).ToList()
        };
    }

    public Task<bool> DeleteShoppingCartAsync(int customerId)
    {
        _repo.DeleteByCustomerIdAsync(customerId);
        return Task.FromResult(true);
    }

    public async Task<bool> DeleteShoppingCartItemAsync(int customerId, int itemId)
    {
        // 1) Load the cart (with items) for that customer
        var cart = await _repo.GetByCustomerIdAsync(customerId);
        if (cart == null) 
            return false;

        // 2) Verify item belongs to this cart
        var item = cart.Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null) 
            return false;

        // 3) Delegate to repo
        return await _repo.DeleteItemByIdAsync(itemId);
    }
}
