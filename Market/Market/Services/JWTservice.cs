using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Market.Enums;
using Market.Settings;
using Microsoft.Extensions.Options;

namespace Market.Services;

public class JWTservice(IOptions<JwtSettings> jwtoptions)
{
    private readonly JwtSettings _jwtSettings = jwtoptions.Value;
    public string GenerateToken(Guid userId, UserRole role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretyKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()), new Claim(ClaimTypes.Role, role.ToString()), new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.LifeTimeInMinutes),
            signingCredentials: credentials );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
}


