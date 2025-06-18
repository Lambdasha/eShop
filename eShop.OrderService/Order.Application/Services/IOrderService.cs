using Order.Application.Models;

namespace Order.Application.Services;

public interface IOrderService
{
    Task<int> CreateOrderAsync(CreateOrderDto dto);
    Task<OrderDto?> GetOrderAsync(int id);
    Task CancelOrderAsync(int id);
    Task<PaginatedResult<OrderDto>> GetAllOrdersAsync(int page, int size);
    Task<IEnumerable<OrderDto>> GetOrdersByCustomerAsync(int customerId);
}