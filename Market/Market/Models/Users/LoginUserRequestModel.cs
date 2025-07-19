namespace Market.Models.Users;

public class LoginUserRequestModel
{
    public required string Identifier { get; set; } // Email або Phone
    public required string Password { get; set; }
}