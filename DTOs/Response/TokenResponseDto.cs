namespace rest_api_custom_jwt_auth.DTOs.Response
{
    public class TokenResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
