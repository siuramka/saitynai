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
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(UserManager<ShopUser> userManager, IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
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

            return Ok(new AuthDtos.SuccessfulLoginDto(accessToken));
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

            return Ok(new AuthDtos.SuccessfulLoginDto(accessToken));
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

        return Ok(new AuthDtos.SuccessfulLoginDto(accessToken));
    }
}