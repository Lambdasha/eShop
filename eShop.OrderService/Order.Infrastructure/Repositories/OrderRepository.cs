using Microsoft.EntityFrameworkCore;
using Order.Domain.Repositories;
using Order.Domain.Entities;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Repositories;

public class OrderRepository : BaseRepository<Domain.Entities.Order>, IOrderRepository
{
    public OrderRepository(OrderDbContext ctx) : base(ctx) { }

    public async Task<IEnumerable<Domain.Entities.Order>> GetByCustomerIdAsync(
        int customerId,
        CancellationToken ct = default)
    {
        return await _ctx.Orders         
            .Include(o => o.Details)
            .Where(o => o.CustomerId == customerId)
            .AsNoTracking()
            .ToListAsync(ct);
    }
}