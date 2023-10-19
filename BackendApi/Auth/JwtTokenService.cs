using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackendApi.Auth.Handlers;
using BackendApi.Auth.Models;
using Microsoft.IdentityModel.Tokens;

namespace BackendApi.Auth;

public class JwtTokenService : IJwtTokenService
{
    private readonly SymmetricSecurityKey _authSigningKey;
    private readonly string _issuer;
    private readonly string _audience;

    public JwtTokenService(IConfiguration configuration)
    {
        _authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
        _issuer = configuration["JWT:ValidIssuer"];
        _audience = configuration["JWT:ValidAudience"];
    }

    public string CreateAccessToken(string email, string userId, IEnumerable<string> userRoles)
    {
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Email, email),
            new(CustomClaims.UserId, userId),
        };
        
        authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

        var accessSecurityToken = new JwtSecurityToken
        (
            issuer: _issuer,
            audience: _audience,
            expires: DateTime.UtcNow.AddDays(999), //TODO: change later
            claims: authClaims,
            signingCredentials: new SigningCredentials(_authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        
        return new JwtSecurityTokenHandler().WriteToken(accessSecurityToken);
    }
}