using System;
using System.Collections.Generic;

namespace rest_api_custom_jwt_auth.Models
{
    public class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int IdUser { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpirationDate { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
