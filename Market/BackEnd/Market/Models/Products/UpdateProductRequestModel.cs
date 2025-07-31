using ShopP21.Enums;

namespace ShopP21.Models.Products;

public class UpdateProductRequestModel
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public ProductCategory Category { get; set; }
}