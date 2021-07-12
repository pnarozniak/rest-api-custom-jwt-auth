using System;

namespace rest_api_custom_jwt_auth.Models
{
    public class User
    {
        public int IdUser { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpirationDate { get; set; }
    }
}
