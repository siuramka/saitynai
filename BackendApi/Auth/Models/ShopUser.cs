namespace BackendApi.Auth.Models;

public partial class ShopUser //: IdentityUser
{
    public int Id { get; set; }
    public string Email { get; set; }
}
public partial class ShopUser
{
    // public ICollection<Shop> Shops { get; set; }
    // public ICollection<Subscription> Subscriptions { get; set; }
    // public ICollection<Rating> Ratings { get; set; }
}