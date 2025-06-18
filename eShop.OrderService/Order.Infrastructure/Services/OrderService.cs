using Microsoft.EntityFrameworkCore;
using Order.Application.Models;
using Order.Application.Services;
using Order.Domain.Entities;
using Order.Domain.Repositories;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
        => _orderRepository = orderRepository;

    public async Task<int> CreateOrderAsync(CreateOrderDto dto)
    {
        // 1) Map DTO → entity
        var order = new Domain.Entities.Order
        {
            CustomerId = dto.CustomerId,
            CustomerName = dto.CustomerName,
            ShippingAddress = dto.ShippingAddress,
            ShippingMethod = dto.ShippingMethod,
            OrderDate = DateTime.UtcNow,
            BillAmount = dto.Details.Sum(x => x.Price * x.Qty - x.Discount),
            Status = Domain.Entities.Order.OrderStatus.Pending // <— use the enum directly
        };

        order.Details = dto.Details
            .Select(d => new OrderDetail
            {
                ProductId = d.ProductId,
                ProductName = d.ProductName,
                Qty = d.Qty,
                Price = d.Price,
                Discount = d.Discount
            })
            .ToList();

        // 2) Use repository
        _orderRepository.Orders.Append(order); // or _orderRepository.Orders.Add(order)
        await _orderRepository.SaveChangesAsync();

        return order.Id;
    }

    public async Task<OrderDto?> GetOrderAsync(int id)
    {
        var o = await _orderRepository.Orders
            .Include(x => x.Details)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (o == null) return null;

        return new OrderDto
        {
            Id = o.Id,
            OrderDate = o.OrderDate,
            CustomerId = o.CustomerId,
            CustomerName = o.CustomerName,
            ShippingAddress = o.ShippingAddress,
            ShippingMethod = o.ShippingMethod,
            BillAmount = o.BillAmount,
            Status = o.Status.ToString(),
            Details = o.Details.Select(d => new OrderDetailDto
            {
                ProductId = d.ProductId,
                ProductName = d.ProductName,
                Qty = d.Qty,
                Price = d.Price,
                Discount = d.Discount
            }).ToList()
        };
    }

    public async Task CancelOrderAsync(int id)
    {
        var o = await _orderRepository.Orders.FindAsync(id)
                ?? throw new KeyNotFoundException("Order not found");
        o.Status = Domain.Entities.Order.OrderStatus.Cancelled;
        await _orderRepository.SaveChangesAsync();
    }

    public async Task<PaginatedResult<OrderDto>> GetAllOrdersAsync(int page, int size)
    {
        var query = _orderRepository.Orders
            .OrderByDescending(x => x.OrderDate)
            .Include(x => x.Details);

        var total = await query.CountAsync();
        var list = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();

        return new PaginatedResult<OrderDto>
        {
            Page = page,
            Size = size,
            TotalCount = total,
            Items = list.Select(o => new OrderDto
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                CustomerId = o.CustomerId,
                CustomerName = o.CustomerName,
                ShippingAddress = o.ShippingAddress,
                ShippingMethod = o.ShippingMethod,
                BillAmount = o.BillAmount,
                Status = o.Status.ToString(),
                Details = o.Details.Select(d => new OrderDetailDto
                {
                    ProductId = d.ProductId,
                    ProductName = d.ProductName,
                    Qty = d.Qty,
                    Price = d.Price,
                    Discount = d.Discount
                }).ToList()
            }).ToList()
        };
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerAsync(int customerId)
    {
        var list = await _orderRepository.Orders
            .Where(x => x.CustomerId == customerId)
            .Include(x => x.Details)
            .ToListAsync();

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
            Details = o.Details.Select(d => new OrderDetailDto
            {
                ProductId = d.ProductId,
                ProductName = d.ProductName,
                Qty = d.Qty,
                Price = d.Price,
                Discount = d.Discount
            }).ToList()
        });
    }
}