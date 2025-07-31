using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.IdentityModel.JsonWebTokens;
using ShopP21.Models.Auth;
using ShopP21.Services;

namespace ShopP21.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(UserService userService, JwtService jwtService) : ControllerBase
{
    // Реєстрація користувача
    [HttpPost("register")]
    public ActionResult<RegisterResponseModel> Register([FromBody] RegisterRequestModel model)
    {
        var createdUser = userService.CreateUser(model.Phone, model.Email, model.Password);

        return Ok(new RegisterResponseModel
        {
            Token = jwtService.GenerateToken(createdUser.Id, createdUser.Role)
        });
    }

    // Логін
    [HttpPost("login")]
    public ActionResult<LoginResponseModel> Login([FromBody] LoginRequestModel model)
    {
        var user = userService.GetUserByCredentials(model.PhoneOrEmail, model.Password);

        return Ok(new LoginResponseModel
        {
            Token = jwtService.GenerateToken(user.Id, user.Role)
        });
    }

    [Authorize] // тільки авторизовані користувачі, або 401
    [HttpGet("profile")]
    public ActionResult<ProfileResponseModel> Profile()
    {
        // витягуємо id користувача з jwt токену
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        // отримуємо користувача по id
        var user = userService.GetUserById(userId);
        
        return Ok(new ProfileResponseModel
        {
            Id = user.Id,
            Email = user.Email,
            Phone = user.Phone,
            CreatedAt = user.CreatedAt
        });
    }
}