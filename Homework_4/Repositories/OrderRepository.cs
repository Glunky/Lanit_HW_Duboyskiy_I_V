using Homework_4.DbModels;
using Homework_4.Provider;
using Microsoft.EntityFrameworkCore;

namespace Homework_4.Repositories;

public class OrderRepository : Repository
{
    public OrderRepository(string connectionString, PurchaseDbContext dbProvider) : base(connectionString, dbProvider)
    {
    }

    public async Task CreateOrder(DbOrder order)
    {
        DbContext.Orders.Add(order);
    }

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }

    public async Task<DbOrder[]> ReadAllOrders()
    {
        return await DbContext.Orders
            .Include(o=>o.Customer)
            .Include(o => o.Products)
            .AsNoTracking()
            .ToArrayAsync();
    }

    public async Task<DbOrder> ReadOrder(Guid id)
    {
        return await DbContext.Orders.FindAsync(id);
    }
     
    public async Task UpdateOrder(DbOrder order, DateTime date)
    {
        order.Date = date;

        await SaveChangesAsync();
    }

    public async Task DeleteOrder(DbOrder order)
    {
        DbContext.Orders.Remove(order);
        
        await SaveChangesAsync();
    }
}
