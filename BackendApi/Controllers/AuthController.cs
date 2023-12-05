using System.Security.Claims;
using BackendApi.Auth;
using BackendApi.Auth.Handlers;
using BackendApi.Auth.Models;
using BackendApi.Data.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBaseWithUserId
{
    private readonly UserManager<ShopUser> _userManager;
    private readonly JwtTokenService _jwtTokenService;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<ShopUser> userManager, JwtTokenService jwtTokenService, IConfiguration configuration)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("register/user")]
    public async Task<IActionResult> RegisterUser([FromBody] AuthDtos.RegisterUserDto registerUserDto)
    {
        var userTaken = await _userManager.FindByEmailAsync(registerUserDto.Email);

        if (userTaken != null)
        {
            return BadRequest();
        }

        var newUser = new ShopUser
        {
            Email = registerUserDto.Email,
            UserName = registerUserDto.Email
        };

        var createUserResult = await _userManager.CreateAsync(newUser, registerUserDto.Password);

        if (createUserResult.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, ShopUserRoles.ShopSeller);
            var roles = await _userManager.GetRolesAsync(newUser);

            var accessToken = _jwtTokenService.CreateAccessToken(newUser.Email, newUser.Id, roles);
            var createdUser = await _userManager.FindByEmailAsync(newUser.Email);

            await UpdateUsersRefreshTokenWithExpiration(createdUser);

            return Ok(new AuthDtos.SuccessfulLoginDto(accessToken, createdUser.RefreshToken));
        }

        return BadRequest();
    }

    [HttpPost]
    [Route("register/seller")]
    public async Task<IActionResult> RegisterSeller([FromBody] AuthDtos.RegisterUserDto registerUserDto)
    {
        var userTaken = await _userManager.FindByEmailAsync(registerUserDto.Email);

        if (userTaken != null)
        {
            return BadRequest();
        }

        var newUser = new ShopUser
        {
            Email = registerUserDto.Email,
            UserName = registerUserDto.Email
        };

        var createUserResult = await _userManager.CreateAsync(newUser, registerUserDto.Password);

        if (createUserResult.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, ShopUserRoles.ShopSeller);
            var roles = await _userManager.GetRolesAsync(newUser);

            var accessToken = _jwtTokenService.CreateAccessToken(newUser.Email, newUser.Id, roles);
            var createdUser = await _userManager.FindByEmailAsync(newUser.Email);

            await UpdateUsersRefreshTokenWithExpiration(createdUser);

            return Ok(new AuthDtos.SuccessfulLoginDto(accessToken, createdUser.RefreshToken));
        }

        return BadRequest();
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login([FromBody] AuthDtos.LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return BadRequest();
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!isPasswordValid)
        {
            return BadRequest("User name or password is invalid.");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = _jwtTokenService.CreateAccessToken(user.Email, user.Id, roles);

        await UpdateUsersRefreshTokenWithExpiration(user);

        return Ok(new AuthDtos.SuccessfulLoginDto(accessToken, user.RefreshToken));
    }


    [Authorize]
    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> Refresh(AuthDtos.RefreshTokenDto refreshTokenDto)
    {
        ClaimsPrincipal? principal;

        try
        {
            principal = _jwtTokenService.GetPrincipalFromExpiredToken(refreshTokenDto.AccessToken);
        }
        catch
        {
            return BadRequest("Invalid token");
        }

        var userID = principal.FindFirst(CustomClaims.UserId).Value;
        var user = await _userManager.FindByIdAsync(userID);

        var isRefreshTokenInvalid = user.RefreshToken != refreshTokenDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow;

        if (user == null || isRefreshTokenInvalid)
        {
            return BadRequest();
        }

        var roles = await _userManager.GetRolesAsync(user);

        var newAccessToken = _jwtTokenService.CreateAccessToken(user.UserName!, user.Id, roles);

        user.RefreshToken = _jwtTokenService.CreateRefreshToken();

        await _userManager.UpdateAsync(user);

        return Ok(new AuthDtos.SuccessfulLoginDto(newAccessToken, user.RefreshToken));
    }

    private async Task UpdateUsersRefreshTokenWithExpiration(ShopUser user)
    {
        _ = int.TryParse(_configuration["JWT:RefreshTokenValidityDays"], out int refreshTokenValidityDays);

        user.RefreshToken = _jwtTokenService.CreateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenValidityDays);

        await _userManager.UpdateAsync(user);
    }
}