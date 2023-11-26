using Homework_4.Provider;

namespace Homework_4.Repositories;

public abstract class Repository
{
    protected PurchaseDbContext DbContext { get; }

    protected Repository(PurchaseDbContext dbContext)
    {
        DbContext = dbContext;
    }
}
