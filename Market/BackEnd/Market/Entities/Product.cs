using ShopP21.Enums;

namespace ShopP21.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public ProductCategory Category { get; set; }
    
    public List<UserProduct>? UsersProduct { get; set; }
}