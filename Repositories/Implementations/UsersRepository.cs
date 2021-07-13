using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using rest_api_custom_jwt_auth.Data;
using rest_api_custom_jwt_auth.Models;
using rest_api_custom_jwt_auth.Repositories.Interfaces;
using rest_api_custom_jwt_auth.Utils;

namespace rest_api_custom_jwt_auth.Repositories.Implementations
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _context;
        public UsersRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RegisterResult> RegisterNewUserAsync(string email, string password)
        {
            var isEmailTaken = await _context.Users.AnyAsync(u => u.Email == email);
            if (isEmailTaken)
                return RegisterResult.EmailIsTaken;

            var user = new User
            {
                Email = email
            };
            user.HashedPassword = new PasswordHasher<User>().HashPassword(user, password);

            await _context.AddAsync(user);

            var isAdded = await _context.SaveChangesAsync() > 0;
            return isAdded ? RegisterResult.Success : RegisterResult.DbError;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await GetUserWithDetailsByAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await GetUserWithDetailsByAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task<bool> UpdateUserRefreshTokenAsync(
            User user, string refreshToken, DateTime refreshTokenExpirationDate)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpirationDate = refreshTokenExpirationDate;

            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<User> GetUserWithDetailsByAsync(
            Expression<Func<User, bool>> predicate)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.IdRoleNavigation)
                .SingleOrDefaultAsync(predicate);
        }
    }
}
