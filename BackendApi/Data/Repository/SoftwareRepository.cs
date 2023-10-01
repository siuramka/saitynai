using BackendApi.Data.Dtos.Shop;
using BackendApi.Data.Dtos.Software;
using BackendApi.Data.Entities;
using BackendApi.Data.Repository.Contracts;
using BackendApi.Helpers;
using BackendApi.Helpers.Sorting;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Data.Repository;

public class SoftwareRepository : RepositoryBase<Software>, ISoftwareRepository
{
    private ISortHelper<Software> _sortHelper;
    public SoftwareRepository(ShopDbContext shopDbContext, ISortHelper<Software> sortHelper) : base(shopDbContext)
    {
        _sortHelper = sortHelper;
    }
    
    public async Task<PagedList<Software>> GetAllSoftwaresPagedAsync(SoftwareParameters parameters)
    {
        var queryable = FindAll().OrderBy(software => software.Name).Include(y => y.Shop);
        var queryableSorted = _sortHelper.ApplySort(queryable, parameters.OrderBy);

        return await PagedList<Software>.CreateAsync(queryableSorted, parameters.PageNumber, parameters.PageSize);
    }
    
    public async Task<PagedList<Software>> GetAllSoftwaresPagedAsync(SoftwareParameters parameters, int shopId)
    {
        var queryable = FindByCondition(software => software.ShopId == shopId).Include(y => y.Shop).OrderBy(software => software.Name);
        var queryableSorted = _sortHelper.ApplySort(queryable, parameters.OrderBy);

        return await PagedList<Software>.CreateAsync(queryableSorted, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<Software?> GetSoftwareByIdAsync(int softwareId, int shopId)
    {
        return await FindByCondition(software => software.Id == softwareId && software.ShopId == shopId).Include(y => y.Shop).FirstOrDefaultAsync();
    }
}