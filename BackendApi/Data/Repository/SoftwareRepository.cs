using System.Linq.Dynamic.Core;
using BackendApi.Data.Dtos.Shop;
using BackendApi.Data.Dtos.Software;
using BackendApi.Data.Entities;
using BackendApi.Data.Repository.Contracts;
using BackendApi.Helpers;
using BackendApi.Data.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Data.Repository;

public class SoftwareRepository : RepositoryBase<Software>, ISoftwareRepository
{
    public SoftwareRepository(ShopDbContext shopDbContext) : base(shopDbContext)
    {
    }
    
    public async Task<PagedList<Software>> GetAllSoftwaresPagedAsync(SoftwareParameters parameters)
    {
        var queryableSorted = FindAll().OrderBy(software => software.Name).Include(y => y.Shop).Sort(parameters.OrderBy);
        
        return await PagedList<Software>.CreateAsync(queryableSorted, parameters.PageNumber, parameters.PageSize);
    }

    /// <summary>
    /// Get all softwares of user by shop id
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<PagedList<Software>> GetAllSoftwaresPagedAsync(SoftwareParameters parameters,  string userId)
    {
        var queryableSorted = FindAll().Include(y => y.Shop).ThenInclude(x => x.ShopUser)
            .Where(y => y.Shop.ShopUser.Id == userId).Sort(parameters.OrderBy);
        
        return await PagedList<Software>.CreateAsync(queryableSorted, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<PagedList<Software>> GetAllSoftwaresPagedAsync(SoftwareParameters parameters, int shopId)
    {
        var queryableSorted = FindByCondition(software => software.ShopId == shopId).Include(y => y.Shop).Sort(parameters.OrderBy);

        return await PagedList<Software>.CreateAsync(queryableSorted, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<Software?> GetSoftwareByIdAsync(int softwareId, int shopId)
    {
        return await FindByCondition(software => software.Id == softwareId && software.ShopId == shopId).Include(y => y.Shop).FirstOrDefaultAsync();
    }
}