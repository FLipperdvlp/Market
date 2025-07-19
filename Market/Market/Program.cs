using System.Text;
using Market.Middleware;
using Market.Persistence;
using Market.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    
    
    builder.Services.AddScoped<ProductService>();
    builder.Services.AddDbContext<MarketContext>();
    builder.Services.AddScoped<UserService>();
    builder.Services.AddScoped<JWTservice>();
    
    
    builder.Services.AddSwaggerGen();
    
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = "MyAppIssuer",
                ValidAudience = "MyAppAudience",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-very-very-very-very-secure-secret-key-123456")),
            };
        });

    builder.Services.AddAuthorization();
}

var app = builder.Build();
{
    // До початку пайплайну додаємо обробник exception-ів
    app.UseCustomExceptionHandlingMiddleware();

    app.UseAuthentication();
    app.UseAuthorization();
    
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.MapControllers();
    app.Run();
}