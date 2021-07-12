using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using rest_api_custom_jwt_auth.Extensions;

namespace rest_api_custom_jwt_auth.Models.Configurations
{
    public class JwtConfiguration
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string SecretKey { get; set; }
        public int RefreshTokenValidityInMinutes { get; set; }
        public int AccessTokenValidityInMinutes { get; set; }

        public Task OnAuthenticationFailedHandler(AuthenticationFailedContext ctx)
        {
            if (ctx.Exception is not null && ctx.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                ctx.Response.Headers.Add("Token-expired", "true");
            }

            return Task.CompletedTask;
        }

        public Task OnTokenValidatedHandler(TokenValidatedContext ctx)
        {
            try
            {
                ctx.HttpContext.Request
                    .SetUserId(ctx.Principal);
            }
            catch
            {
                ctx.Response.StatusCode = 401;
                ctx.Response.CompleteAsync();
            }

            return Task.CompletedTask;
        }
    }
}
