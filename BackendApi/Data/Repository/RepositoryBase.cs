using System.Linq.Expressions;
using BackendApi.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Data.Repository;

public abstract class RepositoryBase<T> : IRepository<T> where T : class
{
    private ShopDbContext _shopDbContext;

    protected RepositoryBase(ShopDbContext shopDbContext)
    {
        _shopDbContext = shopDbContext;
    }

    protected IQueryable<T> FindAll()
    {
        return _shopDbContext.Set<T>().AsNoTracking();
    }

    protected IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return _shopDbContext.Set<T>().Where(expression).AsNoTracking();
    }

    public void Create(T entity)
    {
        _shopDbContext.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _shopDbContext.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _shopDbContext.Set<T>().Remove(entity);
    }
}