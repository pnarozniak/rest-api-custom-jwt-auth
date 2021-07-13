using System.ComponentModel.DataAnnotations;

namespace rest_api_custom_jwt_auth.DTOs.Requests
{
    public class RegisterRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
