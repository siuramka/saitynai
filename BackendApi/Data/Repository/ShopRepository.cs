using BackendApi.Data.Dtos.Shop;
using BackendApi.Data.Entities;
using BackendApi.Data.Repository.Contracts;
using BackendApi.Helpers;
using BackendApi.Helpers.Sorting;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Data.Repository;

public class ShopRepository : RepositoryBase<Shop>, IShopRepository
{
    private ISortHelper<Shop> _sortHelper;
    public ShopRepository(ShopDbContext shopDbContext, ISortHelper<Shop> sortHelper) : base(shopDbContext)
    {
        _sortHelper = sortHelper;
    }

    public async Task<IEnumerable<Shop>> GetAllShopsAsync()
    {
        return await FindAll().OrderBy(shop => shop.Name).ToListAsync();
    }

    public async Task<PagedList<Shop>> GetAllShopsPagedAsync(ShopParameters shopParameters)
    {
        var queryable = FindAll().OrderBy(shop => shop.Name);
        
        var queryableSorted = _sortHelper.ApplySort(queryable, shopParameters.OrderBy);
        
        return await PagedList<Shop>.CreateAsync(queryableSorted, shopParameters.PageNumber,
            shopParameters.PageSize);
    }

    public async Task<Shop?> GetShopByIdAsync(int id)
    {
        return await FindByCondition(shop => shop.Id == id).FirstOrDefaultAsync();
    }
    
}