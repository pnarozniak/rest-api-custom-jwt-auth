using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace rest_api_custom_jwt_auth.Extensions
{
    public static class HttpRequestUserIdExtension
    {
        public static int? IdUser;
        public static void SetUserId(this HttpRequest httpRequest, ClaimsPrincipal claimsPrincipal)
        {
            var nameIdentifier = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (nameIdentifier is null || !int.TryParse(nameIdentifier, out _))
                throw new SecurityTokenDecryptionFailedException();

            IdUser = int.Parse(nameIdentifier);
        }

        public static int? GetUserId(this HttpRequest httpRequest)
        {
            return IdUser;
        }
    }
}
