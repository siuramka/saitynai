using BackendApi.Data.Dtos.Shop;
using BackendApi.Data.Entities;
using BackendApi.Data.Repository.Contracts;
using BackendApi.Helpers;
using Microsoft.EntityFrameworkCore;
using BackendApi.Data.Repository.Extensions;
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
        var queryableSorted = FindAll().Include(x => x.ShopUser).Sort(shopParameters.OrderBy);
        
        return await PagedList<Shop>.CreateAsync(queryableSorted, shopParameters.PageNumber,
            shopParameters.PageSize);
    }

    public async Task<PagedList<Shop>> GetAllShopsPagedAsync(ShopParameters shopParameters, string userId)
    {
        var queryableSorted = FindAll().Include(x => x.ShopUser).Where(x => x.ShopUserId == userId).Sort(shopParameters.OrderBy);
        
        return await PagedList<Shop>.CreateAsync(queryableSorted, shopParameters.PageNumber,
            shopParameters.PageSize);
    }

    public async Task<Shop?> GetShopByIdAsync(int id)
    {
        return await FindByCondition(shop => shop.Id == id).Include(x => x.ShopUser).FirstOrDefaultAsync();
    }
    
}