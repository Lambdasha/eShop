using Order.Application.Models;

namespace Order.Application.Services;

public interface IOrderService
{
    Task<PaginatedResult<OrderDto>> GetAllOrdersAsync(int page, int size);
    Task<IEnumerable<OrderDto>> GetOrdersByCustomerAsync(int customerId);
    Task<int> CreateOrderAsync(CreateOrderDto dto);
    Task CancelOrderAsync(int id);
    Task<OrderDto?> GetOrderByIdAsync(int orderId);
}