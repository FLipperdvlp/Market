using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopP21.Middleware;
using ShopP21.Persistence;
using ShopP21.Services;
using ShopP21.Settings;

var builder = WebApplication.CreateBuilder(args);
{
    var config = builder.Configuration;

    builder.Services.Configure<JwtSettings>(config.GetSection("Jwt"));
    builder.Services.AddSingleton<JwtSettings>();
    
    builder.Services.AddControllers();
    builder.Services.AddDbContext<ShopDbContext>(options =>
    {
        // from appsetings.json from database
        options.UseSqlite(config["DataBase:ConnectionString"]);
    });
    Console.WriteLine("Connection string: " + config["DataBase:ConnectionString"]);
    
    builder.Services.AddScoped<ProductsService>();
    builder.Services.AddScoped<UserService>();
    builder.Services.AddScoped<JwtService>();
    builder.Services.AddScoped<CartService>();
    
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
                IssuerSigningKey = 
                    new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]!)),
            };
        });
    builder.Services.AddAuthorization();
}

var app = builder.Build();
{
    app.UseCustomExceptionHandling();

    app.UseAuthentication();
    app.UseAuthorization();
    
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.MapControllers();
    app.Run();
}