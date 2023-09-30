using BackendApi.Auth.Models;
using Microsoft.AspNetCore.Authorization;

namespace BackendApi.Auth.Middleware;

public class SameUserAuthorizationHandler : AuthorizationHandler<SameUserRequirement, IUserOwnedResource>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameUserRequirement requirement,
        IUserOwnedResource resource)
    {
        if (context.User.IsInRole(ShopUserRoles.Admin) || context.User.FindFirst(CustomClaims.UserId).Value == resource.ShopUserId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public record SameUserRequirement : IAuthorizationRequirement;