using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using rest_api_custom_jwt_auth.DTOs.Requests;
using rest_api_custom_jwt_auth.DTOs.Response;
using rest_api_custom_jwt_auth.Models;
using rest_api_custom_jwt_auth.Repositories.Interfaces;
using rest_api_custom_jwt_auth.Services.Interfaces;

namespace rest_api_custom_jwt_auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ITokensService _tokensService;
        public AccountsController(
            IUsersRepository usersRepository,
            ITokensService tokensService)
        {
            _usersRepository = usersRepository;
            _tokensService = tokensService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _usersRepository.GetUserByEmailAsync(loginRequestDto.Email);
            if (user is null)
                return NotFound();

            var passwordVerificationResult = new PasswordHasher<User>().VerifyHashedPassword(
                user, user.HashedPassword, loginRequestDto.Password);
            if (passwordVerificationResult != PasswordVerificationResult.Success)
                return NotFound();

            var (refreshToken, refreshTokenExpirationDate) = _tokensService.GenerateRefreshToken();
            var accessToken = _tokensService.GenerateAccessTokenForUser(user);

            var isUpdated = await _usersRepository.UpdateUserRefreshTokenAsync
                (user, refreshToken, refreshTokenExpirationDate);
            if (!isUpdated)
                return StatusCode((int)HttpStatusCode.InternalServerError);

            return Ok(new TokenResponseDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken = refreshToken
            });
        }
    }
}
