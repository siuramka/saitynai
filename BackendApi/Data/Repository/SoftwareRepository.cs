using BackendApi.Data.Dtos.Shop;
using BackendApi.Data.Dtos.Software;
using BackendApi.Data.Entities;
using BackendApi.Data.Repository.Contracts;
using BackendApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Data.Repository;

public class SoftwareRepository : RepositoryBase<Software>, ISoftwareRepository
{
    public SoftwareRepository(ShopDbContext shopDbContext) : base(shopDbContext)
    {
    }
    
    public async Task<PagedList<Software>> GetAllSoftwaresPagedAsync(SoftwareParameters parameters)
    {
        var queryable = FindAll().OrderBy(software => software.Name);
        
        return await PagedList<Software>.CreateAsync(queryable, parameters.PageNumber, parameters.PageSize);
    }
    
    public async Task<PagedList<Software>> GetAllSoftwaresPagedAsync(SoftwareParameters parameters, int shopId)
    {
        var queryable = FindByCondition(software => software.ShopId == shopId).OrderBy(software => software.Name);
        
        return await PagedList<Software>.CreateAsync(queryable, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<Software?> GetSoftwareByIdAsync(int softwareId)
    {
        return await FindByCondition(software => software.Id == softwareId).FirstOrDefaultAsync();
    }
}