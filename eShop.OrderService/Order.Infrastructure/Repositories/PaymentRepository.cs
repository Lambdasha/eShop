using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Domain.Repositories;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Repositories;

public class PaymentRepository : BaseRepository<Domain.Entities.Order>, IPaymentRepository
{
    public PaymentRepository(OrderDbContext dbContext): base(dbContext)
    {
        
    }
    public async Task<List<Domain.Entities.Order>> GetByCustomerIdAsync(int customerId,
        CancellationToken ct = default)
    {
        return await _ctx.Orders
            .AsNoTracking()
            .Include(o => o.Details)
            .Where(o => o.CustomerId == customerId)
            .ToListAsync(ct);
    }
}