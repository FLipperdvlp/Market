using System.Security.Claims;
using Market.Models.Auth;
using Market.Models.Users;
using Market.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;


[ApiController]
[Route("auth")]
public class AuthController(UserService userService, JWTservice jwtService) : ControllerBase
{
    // Реєстрація користувача
    [HttpPost("register")]
    public ActionResult<RegisterResponseModel> Register([FromBody] RegisterRequestModel model)
    {
        var createdUser = userService.AddUser(model.Phone, model.Email, model.Password);

        return Ok(new RegisterResponseModel
        {
            Token = jwtService.GenerateToken(createdUser.Id, createdUser.Role)
        });
    }

    // Логін
    [HttpPost("login")]
    public ActionResult<LoginResponseModel> Login([FromBody] LoginUserRequestModel model)
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
            ReleaseDate = user.CreatedAt
        });
    }
}