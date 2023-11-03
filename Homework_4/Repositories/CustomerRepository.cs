using Homework_4.DbModels;
using Homework_4.Provider;
using Microsoft.EntityFrameworkCore;

namespace Homework_4.Repositories;

public class CustomerRepository : Repository
{
    public CustomerRepository(string connectionString, PurchaseDbContext dbProvider) : base(connectionString, dbProvider)
    {
    }

    public async Task CreateCustomer(DbCustomer customer)
    {
        await DbContext.AddAsync(customer);
        await DbContext.SaveChangesAsync();
    }

    public async Task<DbCustomer[]> ReadAllCustomers()
    {
        return await DbContext.Customers.ToArrayAsync();
    }

    public async Task<DbCustomer> ReadCustomer(Guid id)
    {
        return await DbContext.Customers.FindAsync(id);
    }

    public async Task UpdateCustomer(DbCustomer customer, string firstName, string lastName)
    {
        customer.FirstName = firstName;
        customer.LastName = lastName;

        await DbContext.SaveChangesAsync();
    }

    public async Task DeleteCustomer(DbCustomer customer)
    {
        DbContext.Customers.Remove(customer);
        await DbContext.SaveChangesAsync();
    }
}
