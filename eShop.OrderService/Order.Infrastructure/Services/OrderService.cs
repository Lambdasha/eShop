using Microsoft.EntityFrameworkCore;
using Order.Application.Models;
using Order.Application.Services;
using Order.Domain.Entities;
using Order.Domain.Repositories;

namespace Order.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repo;

    public OrderService(IOrderRepository repo)
        => _repo = repo;

    public async Task<int> CreateOrderAsync(CreateOrderDto dto)
    {
        var order = new Domain.Entities.Order
        {
            CustomerId = dto.CustomerId,
            CustomerName = dto.CustomerName,
            ShippingAddress = dto.ShippingAddress,
            ShippingMethod = dto.ShippingMethod,
            OrderDate = DateTime.UtcNow,
            BillAmount = dto.Details.Sum(d => d.Price * d.Qty - d.Discount),
            Status = Domain.Entities.Order.OrderStatus.Pending,
            Details = dto.Details
                .Select(d => new OrderDetail
                {
                    ProductId = d.ProductId,
                    ProductName = d.ProductName,
                    Qty = d.Qty,
                    Price = d.Price,
                    Discount = d.Discount
                })
                .ToList()
        };

        await _repo.AddAsync(order);
        return order.Id;
    }

    public async Task CancelOrderAsync(int id)
    {
        var o = await _repo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Order {id} not found");

        o.Status = Domain.Entities.Order.OrderStatus.Cancelled;
        await _repo.UpdateAsync(o);
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int orderId)
    {
        // Use your overridden GetByIdAsync (which includes Details)
        var o = await _repo
            .Query()
            .FirstOrDefaultAsync(x => x.Id == orderId);

        if (o == null) 
            return null;

        return new OrderDto {
            Id               = o.Id,
            OrderDate        = o.OrderDate,
            CustomerId       = o.CustomerId,
            CustomerName     = o.CustomerName,
            ShippingAddress  = o.ShippingAddress,
            ShippingMethod   = o.ShippingMethod,
            BillAmount       = o.BillAmount,
            Status           = o.Status.ToString(),
            Details          = o.Details
                .Select(d => new OrderDetailDto {
                    ProductId   = d.ProductId,
                    ProductName = d.ProductName,
                    Qty         = d.Qty,
                    Price       = d.Price,
                    Discount    = d.Discount
                })
                .ToList()
        };
    }
    public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerAsync(int customerId)
    {
        var list = await _repo.GetByCustomerIdAsync(customerId);

        return list.Select(o => new OrderDto
        {
            Id = o.Id,
            OrderDate = o.OrderDate,
            CustomerId = o.CustomerId,
            CustomerName = o.CustomerName,
            ShippingAddress = o.ShippingAddress,
            ShippingMethod = o.ShippingMethod,
            BillAmount = o.BillAmount,
            Status = o.Status.ToString(),
            Details = o.Details
                .Select(d => new OrderDetailDto
                {
                    ProductId = d.ProductId,
                    ProductName = d.ProductName,
                    Qty = d.Qty,
                    Price = d.Price,
                    Discount = d.Discount
                })
                .ToList()
        });
    }

    public async Task<PaginatedResult<OrderDto>> GetAllOrdersAsync(int page, int size)
    {
        var (orders, total) = await _repo.GetPagedAsync(page, size);

        var dtos = orders.Select(o => new OrderDto
        {
            Id = o.Id,
            OrderDate = o.OrderDate,
            CustomerId = o.CustomerId,
            CustomerName = o.CustomerName,
            ShippingAddress = o.ShippingAddress,
            ShippingMethod = o.ShippingMethod,
            BillAmount = o.BillAmount,
            Status = o.Status.ToString(),
            Details = o.Details
                .Select(d => new OrderDetailDto
                {
                    ProductId = d.ProductId,
                    ProductName = d.ProductName,
                    Qty = d.Qty,
                    Price = d.Price,
                    Discount = d.Discount
                })
                .ToList()
        }).ToList();

        return new PaginatedResult<OrderDto>
        {
            Page = page,
            Size = size,
            TotalCount = total,
            Items = dtos
        };
    }
}
    