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

    public async Task CreateAsync(T entity)
    {
        _shopDbContext.Set<T>().Add(entity);
        await _shopDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _shopDbContext.Set<T>().Update(entity);
        await _shopDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _shopDbContext.Set<T>().Remove(entity);
        await _shopDbContext.SaveChangesAsync();
    }
}