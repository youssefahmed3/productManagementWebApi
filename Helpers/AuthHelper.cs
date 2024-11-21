using System.Text;
using ProductManagement.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
namespace ProductManagement.Helpers
{
    public class AuthHelper
    {
        // private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        public AuthHelper(IConfiguration config)
        {
            
            _config = config;
        }
        public string GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Secret").Value ?? "");
            // Add roles as claims
            List<Claim> claims = new List<Claim>
            {
                new Claim("userId", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email ?? ""),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString()),
            };

            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));
            SymmetricSecurityKey tokenKey = new SymmetricSecurityKey(key);
            SigningCredentials signingCredentials = new SigningCredentials(
                tokenKey, SecurityAlgorithms.HmacSha512Signature
            );

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = signingCredentials,
                Expires = DateTime.Now.AddDays(1)
            };

            SecurityToken token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        /* public async Task AssignRoleToUser(User user, string role)
        {
            var userManager = _userManager;
            if (!await userManager.IsInRoleAsync(user, role))
            {
                await userManager.AddToRoleAsync(user, role);
            }
        } */
    }
}