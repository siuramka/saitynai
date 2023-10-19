using BackendApi.Auth.Models;
using BackendApi.Data.Dtos.Shop;
using BackendApi.Data.Entities;
using BackendApi.Helpers;

namespace BackendApi.Data.Repository.Contracts;

public interface IShopRepository : IRepository<Shop>
{
    Task<IEnumerable<Shop>> GetAllShopsAsync();
    Task<PagedList<Shop>> GetAllShopsPagedAsync(ShopParameters shopParameters);
    
    /// <summary>
    /// Get all shops of user
    /// </summary>
    /// <param name="shopParameters"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<PagedList<Shop>> GetAllShopsPagedAsync(ShopParameters shopParameters, string userId);

    Task<Shop?> GetShopByIdAsync(int id);
}