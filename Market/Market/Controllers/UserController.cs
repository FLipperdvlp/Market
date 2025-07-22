using Market.Models.Auth;
using Market.Models.Users;
using Market.Services;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;


[ApiController]
[Route("users")]
public class UserController(UserService userService) : ControllerBase
{
    //TODO:    REGISTER
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequestModel model)
    {
        try
        {
            var user = userService.AddUser(model.Phone, model.Email, model.Password);

            var responseUser = new UserResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role.ToString(),
            };
            
            return Ok(responseUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    //TODO:    LOGIN
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginUserRequestModel model)
    {
        var user = userService
            .GetAllUsers()
            .FirstOrDefault(u =>
                u.Email == model.PhoneOrEmail || u.Phone == model.PhoneOrEmail);

        if (user == null || string.IsNullOrEmpty(user.PasswordHash))
            return BadRequest("Invalid username or password");

        if (!userService.VerifyArgon2(user.PasswordHash, model.Password))
            return BadRequest("Invalid password");

        var responseUser = new UserResponseModel
        {
            Id = user.Id,
            Email = user.Email,
            Phone = user.Phone,
            Role = user.Role.ToString(),
        };

        return Ok(responseUser);
    }

}