using System.ComponentModel.DataAnnotations;

namespace Market.Models.Users;

public class LoginUserRequestModel
{
    public required string PhoneOrEmail { get; set; }
    public required string Password { get; set; }
}