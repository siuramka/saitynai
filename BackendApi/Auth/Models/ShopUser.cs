using BackendApi.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace BackendApi.Auth.Models;

public class ShopUser : IdentityUser
{
    public ICollection<Shop> Shops { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; }
}