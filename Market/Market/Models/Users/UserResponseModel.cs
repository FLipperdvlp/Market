namespace Market.Models.Users;

public class UserResponseModel
{
    public Guid Id { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
}