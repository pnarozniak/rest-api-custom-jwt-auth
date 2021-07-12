using System;
using System.IdentityModel.Tokens.Jwt;
using rest_api_custom_jwt_auth.Models;

namespace rest_api_custom_jwt_auth.Services.Interfaces
{
    public interface ITokensService
    {
        public Tuple<string, DateTime> GenerateRefreshToken();
        public JwtSecurityToken GenerateAccessTokenForUser(User user);
    }
}
