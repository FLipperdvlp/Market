using Market.Entities;
using Market.Models.Products;
using Market.Models.Users;
using Market.Services;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;


[ApiController]
[Route("[users]")]
public class UserController(UserService userService) : ControllerBase
{
    //TODO:    REGISTER
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterUserRequestModel model)
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
                (u.Email == model.Identifier || u.Phone == model.Identifier));
        
        if(user == null) return BadRequest("Invalid username or password");
        
        if(!userService.VerifyPassword(user, model.Password, user.PasswordHash)) return BadRequest("Invalid password");
        
        return Ok(user);
    }
}