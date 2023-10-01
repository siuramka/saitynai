using BackendApi.Data.Dtos.Subscription;
using BackendApi.Data.Entities;
using BackendApi.Data.Repository.Contracts;
using BackendApi.Helpers;
using BackendApi.Helpers.Sorting;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Data.Repository;

public class SubscriptionRepository : RepositoryBase<Subscription>, ISubscriptionRepository
{
    private ISortHelper<Subscription> _sortHelper;

    public SubscriptionRepository(ShopDbContext shopDbContext, ISortHelper<Subscription> sortHelper) : base(shopDbContext)
    {
        _sortHelper = sortHelper;
    }

    public async Task<PagedList<Subscription>> GetAllSubscriptionsPagedAsync(SubscriptionParameters subscriptionParameters, int shopId, int softwareId)
    {
        var queryable = FindByCondition(x => x.SoftwareId == softwareId && x.Software.ShopId == shopId).Include(y => y.Software).ThenInclude(y => y.Shop);
        var queryableSorted = _sortHelper.ApplySort(queryable, subscriptionParameters.OrderBy);

        return await PagedList<Subscription>.CreateAsync(queryableSorted, subscriptionParameters.PageNumber, subscriptionParameters.PageSize);
    }

    public async Task<PagedList<Subscription>> GetAllSubscriptionsPagedAsync(SubscriptionParameters subscriptionParameters)
    {
        var queryable = FindAll().Include(y => y.Software).ThenInclude(y => y.Shop);
        var queryableSorted = _sortHelper.ApplySort(queryable, subscriptionParameters.OrderBy);

        return await PagedList<Subscription>.CreateAsync(queryableSorted, subscriptionParameters.PageNumber, subscriptionParameters.PageSize);
    }
    
    public async Task<PagedList<Subscription>> GetAllSubscriptionsCancelledPagedAsync(SubscriptionParameters subscriptionParameters)
    {
        var queryable = FindByCondition(x => x.IsCanceled == true).Include(y => y.Software).ThenInclude(y => y.Shop);
        var queryableSorted = _sortHelper.ApplySort(queryable, subscriptionParameters.OrderBy);

        return await PagedList<Subscription>.CreateAsync(queryableSorted, subscriptionParameters.PageNumber,subscriptionParameters.PageSize);
    }

    public async Task<Subscription?> GetSubscriptionByIdAsync(int subscriptionId, int shopId, int softwareId)
    {
        return await FindByCondition(x =>
                x.Id == subscriptionId && x.SoftwareId == softwareId && x.Software.ShopId == shopId).Include(y => y.Software).ThenInclude(y => y.Shop).FirstOrDefaultAsync();
    }
}