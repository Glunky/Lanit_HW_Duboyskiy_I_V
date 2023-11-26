using Homework_4.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Homework_4.Provider;

public class PurchaseDbContext : DbContext
{
    public PurchaseDbContext(){}
    public PurchaseDbContext(DbContextOptions<PurchaseDbContext> options) : base(options)
    { }
    public DbSet<DbCustomer> Customers { get; set; }
    public DbSet<DbProduct> Products { get; set; }
    public DbSet<DbOrder> Orders { get; set; }

    public const string ConnectionString = @"Server=DESKTOP-23099KB\sqlexpress;Database=PurchaseDb;Trusted_Connection=True;Encrypt=False;";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        CreateCustomer(modelBuilder);
        CreateProduct(modelBuilder);
        CreateOrder(modelBuilder);
    }

    private void CreateCustomer(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbCustomer>().HasKey(c => c.Id);
        modelBuilder.Entity<DbCustomer>().Property(c => c.FirstName).HasColumnType("nvarchar").HasMaxLength(50);
        modelBuilder.Entity<DbCustomer>().Property(c => c.LastName).HasColumnType("nvarchar").HasMaxLength(50);
        modelBuilder.Entity<DbCustomer>()
            .HasMany(c => c.Orders)
            .WithOne(o => o.Customer);
    }

    private void CreateProduct(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbProduct>().HasKey(p => p.Id);
        modelBuilder.Entity<DbProduct>().Property(p => p.ProductName).HasColumnType("nvarchar").HasMaxLength(50);
        modelBuilder.Entity<DbProduct>().Property(p => p.Price).HasColumnType("money");
        modelBuilder.Entity<DbProduct>()
            .HasMany(p => p.Orders)
            .WithMany(op => op.Products)
            .UsingEntity(
                "OrdersProducts",
                l => l.HasOne(typeof(DbOrder)).WithMany().HasForeignKey("OrderId").HasPrincipalKey(nameof(DbOrder.Id)),
                r => r.HasOne(typeof(DbProduct)).WithMany().HasForeignKey("ProductId").HasPrincipalKey(nameof(DbProduct.Id)),
                j => j.HasKey("OrderId", "ProductId"));;
    }

    private void CreateOrder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbOrder>().HasKey(o => o.Id);
        modelBuilder.Entity<DbOrder>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);
        modelBuilder.Entity<DbOrder>().Property(o => o.Date).HasColumnType("date");
        modelBuilder.Entity<DbOrder>()
            .HasMany(p => p.Products)
            .WithMany(op => op.Orders);
    }
}