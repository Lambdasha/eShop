using Order.Application.Models;

namespace Order.Application.Services;

public interface IOrderService
{
    Task CancelOrderAsync(int id);
    Task<PaginatedResult<OrderDto>> GetAllOrdersAsync(int page, int size);
    Task<IEnumerable<OrderDto>> GetOrdersByCustomerAsync(int customerId);
}