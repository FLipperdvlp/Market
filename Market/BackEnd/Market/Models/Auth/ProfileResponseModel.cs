namespace ShopP21.Models.Auth;

public class ProfileResponseModel
{
    public required Guid Id { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public required DateTime CreatedAt { get; set; }
}