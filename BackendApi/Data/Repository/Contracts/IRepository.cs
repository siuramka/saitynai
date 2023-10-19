using System.Linq.Expressions;

namespace BackendApi.Data.Repository.Contracts;

public interface IRepository<T>
{
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}