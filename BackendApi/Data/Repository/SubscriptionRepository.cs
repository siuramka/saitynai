using BackendApi.Data.Dtos.Subscription;
using BackendApi.Data.Entities;
using BackendApi.Data.Repository.Contracts;
using BackendApi.Data.Repository.Extensions;
using BackendApi.Helpers;
using BackendApi.Helpers.Sorting;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Data.Repository;

public class SubscriptionRepository : RepositoryBase<Subscription>, ISubscriptionRepository
{

    public SubscriptionRepository(ShopDbContext shopDbContext) : base(shopDbContext)
    {
    }

    public async Task<PagedList<Subscription>> GetAllSubscriptionsPagedAsync(SubscriptionParameters subscriptionParameters, int shopId, int softwareId)
    {
        var queryableSorted = FindByCondition(x => x.SoftwareId == softwareId && x.Software.ShopId == shopId).Include(y => y.Software).ThenInclude(y => y.Shop).Sort(subscriptionParameters.OrderBy);

        return await PagedList<Subscription>.CreateAsync(queryableSorted, subscriptionParameters.PageNumber, subscriptionParameters.PageSize);
    }

    public async Task<PagedList<Subscription>> GetAllSubscriptionsPagedAsync(SubscriptionParameters subscriptionParameters)
    {
        var queryableSorted = FindAll().Include(y => y.Software).ThenInclude(y => y.Shop).Sort(subscriptionParameters.OrderBy);

        return await PagedList<Subscription>.CreateAsync(queryableSorted, subscriptionParameters.PageNumber, subscriptionParameters.PageSize);
    }

    public async Task<PagedList<Subscription>> GetAllSubscriptionsPagedAsync(SubscriptionParameters subscriptionParameters, string userId)
    {
        var queryableSorted = FindAll().Include(y => y.Software).ThenInclude(y => y.Shop).Where(u => u.ShopUser.Id == userId).Sort(subscriptionParameters.OrderBy);

        return await PagedList<Subscription>.CreateAsync(queryableSorted, subscriptionParameters.PageNumber, subscriptionParameters.PageSize);
    }

    public async Task<PagedList<Subscription>> GetAllSubscriptionsCancelledPagedAsync(SubscriptionParameters subscriptionParameters)
    {
        var queryableSorted = FindByCondition(x => x.IsCanceled == true).Include(y => y.Software).ThenInclude(y => y.Shop).Sort(subscriptionParameters.OrderBy);

        return await PagedList<Subscription>.CreateAsync(queryableSorted, subscriptionParameters.PageNumber,subscriptionParameters.PageSize);
    }

    public async Task<Subscription?> GetSubscriptionByIdAsync(int subscriptionId, int shopId, int softwareId)
    {
        return await FindByCondition(x =>
                x.Id == subscriptionId && x.SoftwareId == softwareId && x.Software.ShopId == shopId).Include(y => y.Software).ThenInclude(y => y.Shop).FirstOrDefaultAsync();
    }
}