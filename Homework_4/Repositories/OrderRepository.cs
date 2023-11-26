using Homework_4._5.Controllers;
using Homework_4.DbModels;
using Homework_4.Provider;
using Homework_4.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Homework_4.Repositories;

public class OrderRepository : Repository, IRepository<DbOrder, OrderInfo>
{
    public OrderRepository(PurchaseDbContext dbProvider) : base(dbProvider)
    {
    }

    public async Task Create(DbOrder order)
    {
        DbContext.Orders.Add(order);
    }
    
    public async Task<DbOrder> Read(Guid id)
    {
        return await DbContext.Orders.FindAsync(id);
    }

    public async Task<DbOrder[]> ReadAll()
    {
        return await DbContext.Orders
            .Include(o=>o.Customer)
            .Include(o => o.Products)
            .AsNoTracking()
            .ToArrayAsync();
    }

    public async Task Update(DbOrder order, OrderInfo info)
    {
        order.Date = info.Date;

        await SaveChangesAsync();
    }

    public async Task Delete(DbOrder order)
    {
        DbContext.Orders.Remove(order);
        
        await SaveChangesAsync();
    }
    
    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}
