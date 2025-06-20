// Order.Infrastructure/Repositories/ShoppingCartRepository.cs
namespace Order.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Domain.Repositories;
using Order.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

public class ShoppingCartRepository : BaseRepository<ShoppingCart>, IShoppingCartRepository
{

    public ShoppingCartRepository(OrderDbContext ctx) : base(ctx)
    {
    }

    public Task<ShoppingCart?> GetByCustomerIdAsync(int customerId, CancellationToken ct = default)
        => _ctx.ShoppingCarts
            .Include(c => c.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CustomerId == customerId, ct);

    public async Task DeleteByCustomerIdAsync(int customerId, CancellationToken ct = default)
    {
        var cart = await _ctx.ShoppingCarts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.CustomerId == customerId, ct);
        if (cart != null)
        {
            _ctx.ShoppingCarts.Remove(cart);
            await _ctx.SaveChangesAsync(ct);
        }
    }
    
    public async Task<bool> DeleteItemByIdAsync(int itemId, CancellationToken ct = default)
    {
        var item = await _ctx.ShoppingCartItems.FindAsync(new object[]{ itemId }, ct);
        if (item == null) return false;

        _ctx.ShoppingCartItems.Remove(item);
        await _ctx.SaveChangesAsync(ct);
        return true;
    }
}