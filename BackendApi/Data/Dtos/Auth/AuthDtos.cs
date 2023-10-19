using System.ComponentModel.DataAnnotations;

namespace BackendApi.Data.Dtos.Auth;

public class AuthDtos
{
    public record RegisterUserDto([EmailAddress][Required] string Email, [Required] string Password);

    public record LoginDto([EmailAddress] string Email, string Password);

    public record UserDto(string Id, string Email);

    public record SuccessfulLoginDto(string AccessToken);
}