using System.Security.Claims;
using BackendApi.Auth.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;

public class ControllerBaseWithUserId : ControllerBase
{
    public string? UserId => User.FindFirstValue(CustomClaims.UserId);
    public bool UserHasRole(string roleToCheck) => User.HasClaim(ClaimTypes.Role, roleToCheck);
}