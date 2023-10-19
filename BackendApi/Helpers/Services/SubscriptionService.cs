using BackendApi.Data.Dtos.Subscription;
using BackendApi.Data.Entities;
using BackendApi.Data.Repository.Contracts;

namespace BackendApi.Helpers.Services;

public class SubscriptionService : ISubscriptionService
{
    public async Task<Subscription> GetSubscriptionWithTerms(
        SubscriptionDtos.SubscriptionCreateDto subscriptionCreateDto,
        Subscription subscription, Software software)
    {
        Subscription subscriptionWithTerms = subscription;

        subscription.Start = DateTime.Now;
        subscription.End = DateTime.Now.AddMonths(subscriptionCreateDto.TermInMonths);

        subscriptionWithTerms.TotalPrice = software.PriceMonthly * subscriptionCreateDto.TermInMonths;

        subscriptionWithTerms.SoftwareId = software.Id;
        
        return subscriptionWithTerms;
    }

    public async Task<Subscription> UpdateSubscription(SubscriptionDtos.SubscriptionUpdateDto subscriptionUpdateDto, Subscription subscription, Software software)
    {
        Subscription subscriptionWithTerms = subscription;

        subscription.End = DateTime.Now.AddMonths(subscriptionUpdateDto.TermInMonths);

        subscriptionWithTerms.TotalPrice += software.PriceMonthly * subscriptionUpdateDto.TermInMonths;

        subscriptionWithTerms.SoftwareId = software.Id;
        
        return subscriptionWithTerms;
    }
}