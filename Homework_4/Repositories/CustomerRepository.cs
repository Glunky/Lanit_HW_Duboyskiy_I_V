using Core.Requests;
using Core.Requests.Customers;
using Homework_4._5.Requests;
using Homework_4.DbModels;
using Homework_4.Provider;
using Homework_4.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Homework_4.Repositories;

public class CustomerRepository : Repository, IRepository<DbCustomer, CustomerInfo>
{
    public CustomerRepository(PurchaseDbContext dbProvider) : base(dbProvider)
    {
    }

    public async Task Create(DbCustomer customer)
    {
        DbContext.Add(customer);
        await SaveChangesAsync();
    }

    public async Task<DbCustomer> Read(Guid id)
    {
        return await DbContext.Customers.FindAsync(id);
    }
    
    public async Task<DbCustomer[]> ReadAll()
    {
        return await DbContext.Customers.ToArrayAsync();
    }

    public async Task Update(DbCustomer customer, CustomerInfo info)
    {
        customer.FirstName = info.FirstName;
        customer.LastName = info.LastName;

        await SaveChangesAsync();
    }

    public async Task Delete(DbCustomer customer)
    {
        DbContext.Customers.Remove(customer);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}
