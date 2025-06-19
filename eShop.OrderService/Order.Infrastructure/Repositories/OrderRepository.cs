using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Domain.Repositories;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Repositories;

public class OrderRepository : BaseRepository<Order.Domain.Entities.Order>, IOrderRepository
{
    public OrderRepository(OrderDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<Domain.Entities.Order> Query()
        => _ctx.Orders
            .Include(o => o.Details)
            .AsNoTracking();
    
    public async Task<IEnumerable<Domain.Entities.Order>> GetByCustomerIdAsync(int customerId, CancellationToken ct = default)
    {
        return await Query()
            .Where(o => o.CustomerId == customerId)
            .ToListAsync(ct);
    }
    public async Task<(IEnumerable<Domain.Entities.Order> Orders, int TotalCount)> GetPagedAsync(
        int page,
        int pageSize,
        CancellationToken ct = default)
    {
        var baseQuery = Query()
            .OrderByDescending(o => o.OrderDate);

        var total = await baseQuery.CountAsync(ct);
        var items = await baseQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (items, total);
    }
}
