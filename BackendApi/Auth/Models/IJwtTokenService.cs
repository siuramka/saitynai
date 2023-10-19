﻿namespace BackendApi.Auth.Models
{
    public interface IJwtTokenService
    {
        string CreateAccessToken(string email, string userId, IEnumerable<string> userRoles);
    }
}
