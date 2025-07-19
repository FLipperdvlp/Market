using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Market.Enums;

namespace Market.Services;

public class JWTservice
{
        private const string SecretKey = "your-very-very-very-very-secure-secret-key-123456"; // Должен быть достаточно длинным
        private const string Issuer = "MyAppIssuer";
        private const string Audience = "MyAppAudience";
        private const int TokenLifetimeMinutes = 60;

        public string GenerateToken(Guid userId, UserRole role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(ClaimTypes.Role, role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(TokenLifetimeMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    
}


