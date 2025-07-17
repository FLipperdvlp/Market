using Market.Enums;

namespace Market.Models.Products;

public class UpdateProductRequestModel
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public ProductCategory Category { get; set; }
}