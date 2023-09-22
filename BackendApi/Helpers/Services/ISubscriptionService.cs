using BackendApi.Data.Dtos.Subscription;
using BackendApi.Data.Entities;

namespace BackendApi.Helpers.Services;

public interface ISubscriptionService
{
    public Task<Subscription> GetSubscriptionWithTerms(SubscriptionDtos.SubscriptionCreateDto subscriptionCreateDto,
        Subscription subscription, Software software);
}