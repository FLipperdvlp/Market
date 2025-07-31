using System.ComponentModel.DataAnnotations;

namespace ShopP21.Models.Auth;

public class RegisterRequestModel
{
    [Required, MaxLength(13)]
    public required string Phone { get; set; }
    
    [Required, MaxLength(64)]
    public required string Email { get; set; }
    
    [Required, MaxLength(64)]
    public required string Password { get; set; }
}