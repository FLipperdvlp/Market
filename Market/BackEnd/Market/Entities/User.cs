using ShopP21.Enums;

namespace ShopP21.Entities;

public class User : BaseEntity
{
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public UserRole Role { get; set; } = UserRole.User;
    
    public List<UserProduct>? UserProducts { get; set; }
}