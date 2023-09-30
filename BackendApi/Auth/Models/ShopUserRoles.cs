namespace BackendApi.Auth.Models;

public static class ShopUserRoles
{
    public const string Admin = nameof(Admin);
    public const string ShopUser = nameof(ShopUser);
    public const string ShopSeller = nameof(ShopSeller);

    public static readonly IReadOnlyCollection<string> All = new[] { Admin, ShopUser, ShopSeller };
}