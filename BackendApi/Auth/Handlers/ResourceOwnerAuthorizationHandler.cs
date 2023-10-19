using BackendApi.Auth.Models;
using Microsoft.AspNetCore.Authorization;

namespace BackendApi.Auth.Handlers;

public class ResourceOwnerAuthorizationHandler : AuthorizationHandler<ResourceOwnerRequirement, IUserOwnedResource>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOwnerRequirement requirement,
        IUserOwnedResource resource)
    {
        if (context.User.FindFirst(CustomClaims.UserId).Value == resource.ShopUserId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public record ResourceOwnerRequirement : IAuthorizationRequirement;