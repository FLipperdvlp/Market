using Market.Enums;

namespace Market.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public ProductCategory Category { get; set; }

    public ICollection<UserProduct> UserProduct { get; set; } =  new List<UserProduct>();
}