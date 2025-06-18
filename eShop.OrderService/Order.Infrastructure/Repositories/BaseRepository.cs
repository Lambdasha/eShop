using Microsoft.EntityFrameworkCore;
using Order.Domain.Repositories;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Repositories;

public class BaseRepository<T> : IRepository<T> where T : class
{
    protected readonly OrderDbContext _ctx;

    public BaseRepository(OrderDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default)
        => await _ctx.Set<T>().AsNoTracking().ToListAsync(ct);

    public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _ctx.Set<T>().FindAsync(id, ct);

    public async Task<T> AddAsync(T entity, CancellationToken ct = default)
    {
        _ctx.Set<T>().Add(entity);
        await _ctx.SaveChangesAsync(ct);
        return entity;
    }
    public async Task UpdateAsync(T entity, CancellationToken ct = default)
    {
        _ctx.Entry(entity).State = EntityState.Modified;
        await _ctx.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await GetByIdAsync(id, ct) ?? throw new KeyNotFoundException();
        _ctx.Set<T>().Remove(entity);
        await _ctx.SaveChangesAsync(ct);
    }
}