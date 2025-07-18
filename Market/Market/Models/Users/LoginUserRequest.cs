namespace Market.Models.Users;

public class LoginUserRequest
{
    public required string Identifier { get; set; } // Email або Phone
    public required string Password { get; set; }
}