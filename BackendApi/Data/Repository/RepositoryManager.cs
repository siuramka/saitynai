using BackendApi.Data.Repository.Contracts;

namespace BackendApi.Data.Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly ShopDbContext _shopDbContext;
    private readonly Lazy<IShopRepository> _shopRepository;
    private readonly Lazy<ISoftwareRepository> _softwareRepository;
    private readonly Lazy<ISubscriptionRepository> _subscriptionRepository;

    public RepositoryManager(ShopDbContext shopDbContext)
    {
        _shopDbContext = shopDbContext;
        _shopRepository = new Lazy<IShopRepository>(() => new ShopRepository(shopDbContext));
        _softwareRepository = new Lazy<ISoftwareRepository>(() => new SoftwareRepository(shopDbContext));
        _subscriptionRepository = new Lazy<ISubscriptionRepository>(() => new SubscriptionRepository(shopDbContext));
    }

    public IShopRepository Shops => _shopRepository.Value;
    public ISoftwareRepository Softwares => _softwareRepository.Value;
    public ISubscriptionRepository Subscriptions => _subscriptionRepository.Value;
    
    public async Task SaveAsync() => await _shopDbContext.SaveChangesAsync();
}