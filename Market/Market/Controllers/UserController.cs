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
    public IActionResult Register([FromBody] RegisterUserRequest model)//TODO:      USER RESPONSE MODEL FOR THE BETTER SECURITY
    {
        try
        {
            var user = userService.AddUser(model.Phone, model.Email, model.Password);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    //TODO:    LOGIN
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginUserRequest model)
    {
        var user = userService
            .GetAllUsers()
            .FirstOrDefault(u =>
                (u.Email == model.Identifier || u.Phone == model.Identifier));
        
        if(user == null) return BadRequest("Invalid username or password");
        
        if(!userService.VerifyPassword(model.Password, user.PasswordHash)) return BadRequest("Invalid password");
        
        return Ok(user);
    }
}