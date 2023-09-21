using BackendApi.Data.Dtos.Shop;
using BackendApi.Data.Entities;
using BackendApi.Helpers;

namespace BackendApi.Data.Repository.Contracts;

public interface IShopRepository : IRepository<Shop>
{
    Task<IEnumerable<Shop>> GetAllShopsAsync();
    Task<PagedList<Shop>> GetAllShopsPagedAsync(ShopParameters shopParameters);
    Task<Shop?> GetShopByIdAsync(int id);
}