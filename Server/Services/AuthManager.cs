using helping_hand.Models;
using helping_hand.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace helping_hand.Server.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        private ApiUser _user;

        public AuthManager(UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GetTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = _configuration["ApiKey"];
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.Id)
            };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GetTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var issuer = jwtSettings.GetSection("Issuer").Value;
            var lifetime = jwtSettings.GetSection("Lifetime").Value;
            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(lifetime));

            var token = new JwtSecurityToken(
                issuer: issuer,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
            );

            return token;
        }

        public async Task<bool> ValidateUser(LoginDto loginDto)
        {
            _user = await _userManager.FindByNameAsync(loginDto.UserName);
            return _user is not null && await _userManager.CheckPasswordAsync(_user, loginDto.Password);
        }

        public async Task<bool> ValidateEmail(LoginDto loginDto)
        {
            _user = await _userManager.FindByNameAsync(loginDto.UserName);
            return _user is not null && await _userManager.IsEmailConfirmedAsync(_user);
        }
    }
}
