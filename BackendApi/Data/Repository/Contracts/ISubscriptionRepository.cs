using BackendApi.Data.Dtos.Subscription;
using BackendApi.Data.Entities;
using BackendApi.Helpers;

namespace BackendApi.Data.Repository.Contracts;

public interface ISubscriptionRepository : IRepository<Subscription>
{
    Task<PagedList<Subscription>> GetAllSubscriptionsPagedAsync(SubscriptionParameters subscriptionParameters, int shopId, int softwareId);
    Task<PagedList<Subscription>> GetAllSubscriptionsPagedAsync(SubscriptionParameters subscriptionParameters);

    // Task<PagedList<Subscription>> GetAllSubscriptionsPagedAsync(SubscriptionParameters subscriptionParameters, int userId);

    Task<PagedList<Subscription>> GetAllSubscriptionsCancelledPagedAsync(SubscriptionParameters subscriptionParameters);

    Task<Subscription?> GetSubscriptionByIdAsync( int subscriptionId, int shopId, int softwareId);
}