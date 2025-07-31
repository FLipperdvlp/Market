using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopP21.Enums;
using ShopP21.Settings;

namespace ShopP21.Services;

public class JwtService(IOptions<JwtSettings> jwtOptions)
{
    // З IOptions<JwtSettings> витягуємо JwtSettings
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;
    
    /// <summary>
    /// Згенерувати JWT токен
    /// </summary>
    /// <param name="userId">Айді користувача</param>
    /// <param name="role">Роль користувача</param>
    /// <returns>JWT токен</returns>
    public string GenerateToken(Guid userId, UserRole role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(ClaimTypes.Role, role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.LifetimeInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}