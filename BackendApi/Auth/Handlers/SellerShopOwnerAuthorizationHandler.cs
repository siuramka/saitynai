using System.Security.Claims;
using BackendApi.Auth.Models;
using BackendApi.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace BackendApi.Auth.Handlers;

public class SellerShopOwnerAuthorizationHandler : AuthorizationHandler<SellerShopOwnerRequirement, Shop>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SellerShopOwnerRequirement requirement,
        Shop resource)
    {
        if (context.User.HasClaim(ClaimTypes.Role, ShopUserRoles.ShopSeller)
            && context.User.FindFirst(CustomClaims.UserId).Value == resource.ShopUserId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public record SellerShopOwnerRequirement : IAuthorizationRequirement;