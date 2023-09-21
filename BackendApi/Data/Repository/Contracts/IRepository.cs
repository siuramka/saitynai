using System.Linq.Expressions;

namespace BackendApi.Data.Repository.Contracts;

public interface IRepository<T>
{
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}