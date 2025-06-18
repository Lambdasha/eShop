using Order.Domain.Entities;

namespace Order.Domain.Repositories;

public interface IPaymentRepository : IRepository<Entities.Order>
{
    Task<List<Entities.Order>> GetByCustomerIdAsync(int customerId, CancellationToken ct = default);
}