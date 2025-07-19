using System.ComponentModel.DataAnnotations;

namespace Market.Models.Users;

public class LoginUserRequestModel
{
    [Required, MaxLength(64)]
    public required string PhoneOrEmail { get; set; }
    [Required, MaxLength(64)]
    public required string Password { get; set; }
}