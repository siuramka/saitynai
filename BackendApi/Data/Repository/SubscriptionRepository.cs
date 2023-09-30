using BackendApi.Data.Dtos.Subscription;
using BackendApi.Data.Entities;
using BackendApi.Data.Repository.Contracts;
using BackendApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Data.Repository;

public class SubscriptionRepository : RepositoryBase<Subscription>, ISubscriptionRepository
{
    public SubscriptionRepository(ShopDbContext shopDbContext) : base(shopDbContext)
    {
    }

    public async Task<PagedList<Subscription>> GetAllSubscriptionsPagedAsync(SubscriptionParameters subscriptionParameters, int shopId, int softwareId)
    {
        var queryable = FindByCondition(x => x.SoftwareId == softwareId && x.Software.ShopId == shopId);
        
        return await PagedList<Subscription>.CreateAsync(queryable, subscriptionParameters.PageNumber, subscriptionParameters.PageSize);
    }

    public async Task<PagedList<Subscription>> GetAllSubscriptionsPagedAsync(SubscriptionParameters subscriptionParameters, int userId)
    {
        string userIdString = userId.ToString();
        var queryable = FindByCondition(x => x.ShopUserId == userIdString).Include(y => y.Software);
        
        return await PagedList<Subscription>.CreateAsync(queryable, subscriptionParameters.PageNumber, subscriptionParameters.PageSize);
    }

    public async Task<PagedList<Subscription>> GetAllSubscriptionsCancelledPagedAsync(SubscriptionParameters subscriptionParameters)
    {
        var queryable = FindByCondition(x => x.IsCanceled == true);
        
        return await PagedList<Subscription>.CreateAsync(queryable, subscriptionParameters.PageNumber,subscriptionParameters.PageSize);
    }

    public async Task<Subscription?> GetSubscriptionByIdAsync(int subscriptionId, int shopId, int softwareId)
    {
        return await FindByCondition(x =>
                x.Id == subscriptionId && x.SoftwareId == softwareId && x.Software.ShopId == shopId).FirstOrDefaultAsync();
    }
}