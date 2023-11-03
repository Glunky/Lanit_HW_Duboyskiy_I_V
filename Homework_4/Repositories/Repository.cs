using Homework_4.Provider;

namespace Homework_4.Repositories;

public abstract class Repository
{
    private string ConnectionString { get; }
    protected PurchaseDbContext DbContext { get; }

    protected Repository(string connectionString, PurchaseDbContext dbProvider)
    {
        ConnectionString = connectionString;
        DbContext = dbProvider;
    }
}
