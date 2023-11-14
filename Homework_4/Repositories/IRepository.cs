using Homework_4.DbModels;

namespace Homework_4.Repositories.Interfaces;

public interface IRepository<T, V>
{
    Task Create(T t);
    Task<T> Read(Guid id);
    Task<T[]> ReadAll();
    Task Update(T t, V v);
    Task Delete(T t);
    Task SaveChangesAsync();
}