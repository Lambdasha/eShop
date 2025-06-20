// Order.Infrastructure/Repositories/CustomerRepository.cs
namespace Order.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Domain.Repositories;
using Order.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(OrderDbContext ctx) : base(ctx)
    {
    }

    // ────────── Customer ──────────

    public async Task<Customer> AddCustomerAsync(Customer customer, CancellationToken ct = default)
    {
        await _ctx.Customers.AddAsync(customer, ct);
        await _ctx.SaveChangesAsync(ct);
        return customer;
    }

    public Task<Customer?> GetCustomerByIdAsync(int customerId, CancellationToken ct = default)
        => _ctx.Customers
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.Id == customerId, ct);

    // ────────── UserAddress ──────────

    public IQueryable<UserAddress> QueryUserAddresses()
        => _ctx.UserAddresses
               .Include(ua => ua.Address)
               .AsNoTracking();

    public Task<IEnumerable<UserAddress>> GetAddressesByCustomerIdAsync(
        int customerId,
        CancellationToken ct = default)
        => QueryUserAddresses()
            .Where(ua => ua.CustomerId == customerId)
            .ToListAsync(ct)
            .ContinueWith(t => (IEnumerable<UserAddress>)t.Result, ct);

    public Task<UserAddress?> GetUserAddressByIdAsync(int id, CancellationToken ct = default)
        => QueryUserAddresses()
            .FirstOrDefaultAsync(ua => ua.Id == id, ct);

    public async Task<UserAddress> AddUserAddressAsync(UserAddress ua, CancellationToken ct = default)
    {
        await _ctx.UserAddresses.AddAsync(ua, ct);
        await _ctx.SaveChangesAsync(ct);
        return ua;
    }

    public async Task<UserAddress> UpdateUserAddressAsync(UserAddress ua, CancellationToken ct = default)
    {
        _ctx.UserAddresses.Update(ua);
        await _ctx.SaveChangesAsync(ct);
        return ua;
    }
}
