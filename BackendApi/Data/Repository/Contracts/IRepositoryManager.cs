using BackendApi.Data.Repository.Contracts;

namespace BackendApi.Data.Repository.Contracts;

public interface IRepositoryManager
{
    IShopRepository Shops { get; }
    ISoftwareRepository Softwares { get; }
    ISubscriptionRepository Subscriptions { get; }
    Task SaveAsync();
}