using BackendApi.Auth.Middleware;
using BackendApi.Auth.Models;
using BackendApi.Data.Dtos.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Data.Repository;

public class AuthController : ControllerBase
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
        var user = await _userManager.FindByEmailAsync(registerUserDto.Email);
        if (user != null)
            return BadRequest("Request invalid.");

        var newUser = new ShopUser
        {
            Email = registerUserDto.Email,
        };
        var createUserResult = await _userManager.CreateAsync(newUser, registerUserDto.Password);
        if (!createUserResult.Succeeded)
            return BadRequest("Could not create a user.");

        await _userManager.AddToRoleAsync(newUser, ShopUserRoles.ShopUser);
        
        return CreatedAtAction(nameof(RegisterUser), new AuthDtos.UserDto(newUser.Id, newUser.Email));
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login([FromBody] AuthDtos.LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.UserName);
        if (user == null)
            return BadRequest("User name or password is invalid.");
        
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!isPasswordValid)
            return BadRequest("User name or password is invalid.");
        
        // valid user
        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = _jwtTokenService.CreateAccessToken(user.Email, user.Id.ToString(), roles);
        
        return Ok(new AuthDtos.SuccessfulLoginDto(accessToken));
    }
}