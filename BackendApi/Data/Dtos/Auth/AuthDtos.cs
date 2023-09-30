using System.ComponentModel.DataAnnotations;

namespace BackendApi.Data.Dtos.Auth;

public class AuthDtos
{
    public record RegisterUserDto([EmailAddress][Required] string Email, [Required] string Password);

    public record LoginDto(string UserName, string Password);

    public record UserDto(string Id, string Email);

    public record SuccessfulLoginDto(string AccessToken);
}