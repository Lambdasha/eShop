namespace Order.Domain.Repositories;
using Entities;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId, CancellationToken ct = default);
}