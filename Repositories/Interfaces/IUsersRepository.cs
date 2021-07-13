using System;
using System.Threading.Tasks;
using rest_api_custom_jwt_auth.Models;
using rest_api_custom_jwt_auth.Utils;

namespace rest_api_custom_jwt_auth.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        public Task<RegisterResult> RegisterNewUserAsync(string email, string password);
        public Task<User> GetUserByEmailAsync(string email);

        public Task<User> GetUserByRefreshTokenAsync(string refreshToken);

        public Task<bool> UpdateUserRefreshTokenAsync(
            User user, string refreshToken, DateTime refreshTokenExpirationDate);
    }
}
