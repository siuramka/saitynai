using BackendApi.Data.Dtos.Shop;
using BackendApi.Data.Entities;
using BackendApi.Data.Repository.Contracts;
using BackendApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Data.Repository;

public class ShopRepository : RepositoryBase<Shop>, IShopRepository
{
    public ShopRepository(ShopDbContext shopDbContext) : base(shopDbContext)
    {
    }

    public async Task<IEnumerable<Shop>> GetAllShopsAsync()
    {
        return await FindAll().OrderBy(shop => shop.Name).ToListAsync();
    }

    public async Task<PagedList<Shop>> GetAllShopsPagedAsync(ShopParameters shopParameters)
    {
        var queryable = FindAll().OrderBy(shop => shop.Name);
        return await PagedList<Shop>.CreateAsync(queryable, shopParameters.PageNumber,
            shopParameters.PageSize);
    }

    public async Task<Shop?> GetShopByIdAsync(int id)
    {
        return await FindByCondition(shop => shop.Id == id).FirstOrDefaultAsync();
    }
    
}