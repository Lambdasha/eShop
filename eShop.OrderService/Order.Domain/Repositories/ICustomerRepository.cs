namespace Order.Domain.Repositories;
using Order.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

public interface ICustomerRepository
{
    // ────────── Customer operations ──────────

    /// <summary>Create a new customer (Id is auto‐populated).</summary>
    Task<Customer> AddCustomerAsync(Customer customer, CancellationToken ct = default);

    /// <summary>Fetch one customer by Id, or null if not found.</summary>
    Task<Customer?> GetCustomerByIdAsync(int customerId, CancellationToken ct = default);

    // ────────── UserAddress operations ────────

    /// <summary>
    /// Queryable of UserAddress including the Address nav property.
    /// </summary>
    IQueryable<UserAddress> QueryUserAddresses();

    /// <summary>
    /// List all addresses for a given customer, with Address included.
    /// </summary>
    Task<IEnumerable<UserAddress>> GetAddressesByCustomerIdAsync(
        int customerId,
        CancellationToken ct = default);

    /// <summary>Fetch one UserAddress by its own Id, with Address included.</summary>
    Task<UserAddress?> GetUserAddressByIdAsync(int id, CancellationToken ct = default);

    /// <summary>Insert a new UserAddress (and nested Address).</summary>
    Task<UserAddress> AddUserAddressAsync(UserAddress ua, CancellationToken ct = default);

    /// <summary>Update an existing UserAddress (and nested Address).</summary>
    Task<UserAddress> UpdateUserAddressAsync(UserAddress ua, CancellationToken ct = default);
}