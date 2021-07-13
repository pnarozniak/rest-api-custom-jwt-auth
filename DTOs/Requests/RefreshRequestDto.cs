using System.ComponentModel.DataAnnotations;

namespace rest_api_custom_jwt_auth.DTOs.Requests
{
    public class RefreshRequestDto
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
