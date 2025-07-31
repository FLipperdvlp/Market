using System.ComponentModel.DataAnnotations;

namespace ShopP21.Models.Auth;

public class LoginRequestModel
{
    [Required, MaxLength(64)]
    public required string PhoneOrEmail { get; set; }
    
    [Required, MaxLength(64)]
    public required string Password { get; set; }
}