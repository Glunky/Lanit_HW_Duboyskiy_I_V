using Homework_4.DbModels;
using Homework_4.Provider;
using Microsoft.EntityFrameworkCore;

namespace Homework_4.Repositories;

public class ProductRepository : Repository

{
    public ProductRepository(string connectionString, PurchaseDbContext dbProvider) : base(connectionString, dbProvider)
    {
    }

    public async Task CreateProduct(DbProduct product)
    {
        await DbContext.Products.AddAsync(product);
        await DbContext.SaveChangesAsync();
    }

    public async Task<DbProduct[]> ReadAllProducts()
    {
        return await DbContext.Products.ToArrayAsync();
    }

    public async Task<DbProduct> ReadProduct(Guid id)
    {
        return await DbContext.Products.FindAsync(id);
    }

    public async Task UpdateProduct(DbProduct product, string productName, decimal productPrice)
    {
        product.ProductName = productName;
        product.Price = productPrice;
        
        await DbContext.SaveChangesAsync();
    }

    public async Task DeleteProduct(DbProduct product)
    {
        DbContext.Products.Remove(product);
        
        await DbContext.SaveChangesAsync();
    }
}
