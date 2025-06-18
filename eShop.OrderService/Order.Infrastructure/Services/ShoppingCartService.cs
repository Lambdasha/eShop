using Microsoft.EntityFrameworkCore;
using Order.Application.Models;
using Order.Application.Services;
using Order.Domain.Entities;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly OrderDbContext _db;
    private readonly IOrderService _orderSvc;

    public ShoppingCartService(OrderDbContext db, IOrderService orderSvc)
    {
        _db = db;
        _orderSvc = orderSvc;
    }

    public async Task<ShoppingCartDto> GetCartAsync(int customerId)
    {
        var cart = await _db.ShoppingCarts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);

        if (cart == null)
        {
            cart = new ShoppingCart { CustomerId = customerId };
            _db.ShoppingCarts.Add(cart);
            await _db.SaveChangesAsync();
        }

        return new ShoppingCartDto
        {
            Id = cart.Id,
            CustomerId = cart.CustomerId,
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

    public async Task AddItemAsync(int customerId, AddCartItemDto dto)
    {
        var cartDto = await GetCartAsync(customerId);
        var cart = await _db.ShoppingCarts.Include(c => c.Items)
            .FirstAsync(c => c.Id == cartDto.Id);

        var existing = cart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);
        if (existing != null)
        {
            existing.Qty += dto.Qty;
        }
        else
        {
            cart.Items.Add(new ShoppingCartItem
            {
                ProductId = dto.ProductId,
                ProductName = dto.ProductName,
                Qty = dto.Qty,
                Price = dto.Price
            });
        }

        await _db.SaveChangesAsync();
    }

    public async Task RemoveItemAsync(int cartId, int itemId)
    {
        var item = await _db.ShoppingCartItems
            .FirstOrDefaultAsync(i => i.Id == itemId && i.CartId == cartId);
        if (item == null) throw new KeyNotFoundException("Cart item not found");
        _db.ShoppingCartItems.Remove(item);
        await _db.SaveChangesAsync();
    }

    public async Task<int> CheckoutAsync(int customerId, CheckoutDto dto)
    {
        // 1) Load cart & items
        var cart = await _db.ShoppingCarts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        if (cart == null || !cart.Items.Any())
            throw new InvalidOperationException("Shopping cart is empty.");

        // 2) Build CreateOrderDto
        var create = new CreateOrderDto
        {
            CustomerId = customerId,
            CustomerName = "", // you might fetch name elsewhere
            ShippingAddress = dto.ShippingAddress,
            ShippingMethod = dto.ShippingMethod,
            Details = cart.Items.Select(i => new CreateOrderDetailDto
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Qty = i.Qty,
                Price = i.Price,
                Discount = 0m
            }).ToList()
        };

        // 3) Create order
        var orderId = await _orderSvc.CreateOrderAsync(create);

        // 4) Clear cart
        _db.ShoppingCartItems.RemoveRange(cart.Items);
        await _db.SaveChangesAsync();

        return orderId;
    }
}