using BackendApi.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace BackendApi.Auth.Models;

public class ShopUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public ICollection<Shop> Shops { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; }
}