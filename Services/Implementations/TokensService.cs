using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using rest_api_custom_jwt_auth.Models;
using rest_api_custom_jwt_auth.Models.Configurations;
using rest_api_custom_jwt_auth.Services.Interfaces;

namespace rest_api_custom_jwt_auth.Services.Implementations
{
    public class TokensService : ITokensService
    {
        private readonly JwtConfiguration _jwtConfiguration;
        public TokensService(IOptionsMonitor<JwtConfiguration> optionsMonitor)
        {
            _jwtConfiguration = optionsMonitor.CurrentValue;
        }

        public Tuple<string, DateTime> GenerateRefreshToken()
        {
            return new(
                Guid.NewGuid().ToString(),
                DateTime.Now.AddMinutes(_jwtConfiguration.RefreshTokenValidityInMinutes));
        }

        public JwtSecurityToken GenerateAccessTokenForUser(User user)
        {
            var userClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
            };

            userClaims.AddRange(
                user.UserRoles.Select(ur =>
                    new Claim(ClaimTypes.Role, ur.IdRoleNavigation.Name))
            );

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey));

            return new JwtSecurityToken(
                issuer: _jwtConfiguration.ValidIssuer,
                audience: _jwtConfiguration.ValidAudience,
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(_jwtConfiguration.AccessTokenValidityInMinutes),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = _jwtConfiguration.ValidIssuer,
                ValidAudience = _jwtConfiguration.ValidAudience,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey))
            };

            return ValidateAndGetPrincipalFromJwt(token, tokenValidationParameters);
        }

        private ClaimsPrincipal ValidateAndGetPrincipalFromJwt
            (string token, TokenValidationParameters tokenValidationParameters)
        {
            try
            {
                ClaimsPrincipal principal = new JwtSecurityTokenHandler()
                    .ValidateToken(token, tokenValidationParameters, out var securityToken);

                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
