using Homework_4._5.Requests;
using Homework_4.DbModels;
using Homework_4.Provider;
using Homework_4.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Homework_4.Repositories;

public class ProductRepository : Repository, IRepository<DbProduct, ProductInfo>

{
    public ProductRepository(PurchaseDbContext dbProvider) : base(dbProvider)
    {
    }

    public async Task Create(DbProduct product)
    {
        DbContext.Products.Add(product);
        await SaveChangesAsync();
    }

    public async Task<DbProduct> Read(Guid id)
    {
        return await DbContext.Products.FindAsync(id);
    }
    
    public async Task<DbProduct[]> ReadAll()
    {
        return await DbContext.Products.ToArrayAsync();
    }

    public async Task Update(DbProduct product, ProductInfo info)
    {
        product.ProductName = info.ProductName;
        product.Price = info.Price;
        
        await SaveChangesAsync();
    }

    public async Task Delete(DbProduct product)
    {
        DbContext.Products.Remove(product);
        
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}
