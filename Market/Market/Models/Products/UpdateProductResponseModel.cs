using Market.Entities;

namespace Market.Models.Products;

public class UpdateProductResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public string Category { get; set; } = null!;

    public UpdateProductResponseModel(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Price = product.Price;
        Amount = product.Amount;
        Category = product.Category.ToString();
    }
}