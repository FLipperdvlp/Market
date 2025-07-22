using System.Text;
using Market.Middleware;
using Market.Persistence;
using Market.Services;
using Market.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
{
    var config = builder.Configuration;

    builder.Services.Configure<JwtSettings>(config.GetSection("Jwt"));
    // builder.Services.AddSingleton<JwtSettings>();
    
    builder.Services.AddControllers();
    builder.Services.AddDbContext<MarketContext>(options =>
    {
        // from appsetings.json from database
        options.UseSqlite(config["DataBase:ConnectionString"]);
    });
    Console.WriteLine("Connection string: " + config["DataBase:ConnectionString"]);
    
    builder.Services.AddScoped<ProductService>();
    builder.Services.AddScoped<UserService>();
    builder.Services.AddScoped<JWTservice>();
    builder.Services.AddScoped<UserProductService>();
    
    builder.Services.AddHttpContextAccessor();
    
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

                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]!)),
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