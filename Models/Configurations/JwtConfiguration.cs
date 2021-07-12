namespace rest_api_custom_jwt_auth.Models.Configurations
{
    public class JwtConfiguration
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string SecretKey { get; set; }
        public int RefreshTokenValidityInMinutes { get; set; }
        public int AccessTokenValidityInMinutes { get; set; }
    }
}
