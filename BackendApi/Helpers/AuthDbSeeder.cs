using BackendApi.Auth.Models;
using Microsoft.AspNetCore.Identity;

namespace BackendApi.Helpers;

public class AuthDbSeeder
{
    private readonly UserManager<ShopUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthDbSeeder(UserManager<ShopUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        await AddDefaultRoles();
        await AddAdminUser();
        await AddShopUser();
        await AddShopUserTwo();
        await AddShopSeller();
        await AddShopSellerTwo();

    }

    private async Task AddAdminUser()
    {
        var newAdminUser = new ShopUser()
        {
            Email = "admin@x.com",
            UserName = "admin@x.com"
        };
        
        var existingAdminUser = await _userManager.FindByEmailAsync(newAdminUser.Email);
        if (existingAdminUser == null)
        {
            var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "VerySafePassword1!");
            if (createAdminUserResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(newAdminUser, ShopUserRoles.Admin);
            }
        }
    }
    private async Task AddShopUser()
    {
        var newAdminUser = new ShopUser()
        {
            Email = "shopUser@x.com",
            UserName = "shopUser@x.com"
        };
        
        var existingAdminUser = await _userManager.FindByEmailAsync(newAdminUser.Email);
        if (existingAdminUser == null)
        {
            var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "VerySafePassword1!");
            if (createAdminUserResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(newAdminUser, ShopUserRoles.ShopUser);
            }
        }
    }
    private async Task AddShopUserTwo()
    {
        var newAdminUser = new ShopUser()
        {
            Email = "shopUser2@x.com",
            UserName = "shopUser2@x.com"
        };
        
        var existingAdminUser = await _userManager.FindByEmailAsync(newAdminUser.Email);
        if (existingAdminUser == null)
        {
            var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "VerySafePassword1!");
            if (createAdminUserResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(newAdminUser, ShopUserRoles.ShopUser);
            }
        }
    }
    private async Task AddShopSeller()
    {
        var newAdminUser = new ShopUser()
        {
            Email = "shopSeller@x.com",
            UserName = "shopSeller@x.com"
        };
        
        var existingAdminUser = await _userManager.FindByEmailAsync(newAdminUser.Email);
        if (existingAdminUser == null)
        {
            var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "VerySafePassword1!");
            if (createAdminUserResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(newAdminUser, ShopUserRoles.ShopSeller);
            }
        }
    }
    private async Task AddShopSellerTwo()
    {
        var newAdminUser = new ShopUser()
        {
            Email = "shopSeller2@x.com",
            UserName = "shopSeller2@x.com"
        };
        
        var existingAdminUser = await _userManager.FindByEmailAsync(newAdminUser.Email);
        if (existingAdminUser == null)
        {
            var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "VerySafePassword1!");
            if (createAdminUserResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(newAdminUser, ShopUserRoles.ShopSeller);
            }
        }
    }

    private async Task AddDefaultRoles()
    {
        foreach (var role in ShopUserRoles.All)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
                await _roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}