using Order.Domain.Entities;
namespace Order.Domain.Repositories;

public interface IOrderRepository : IRepository<Entities.Order>
{

    Task<IEnumerable<Entities.Order>> GetByCustomerIdAsync(int customerId, CancellationToken ct = default);
    IQueryable<Entities.Order> Query();

    Task<(IEnumerable<Domain.Entities.Order> Orders, int TotalCount)> GetPagedAsync(
        int page,
        int pageSize,
        CancellationToken ct = default);

}