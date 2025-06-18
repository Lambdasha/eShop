using Order.Domain.Entities;
namespace Order.Domain.Repositories;

public interface IOrderRepository : IRepository<Entities.Order>
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Entities.Order>> GetByCustomerIdAsync(int customerId, CancellationToken ct = default);
    }
}