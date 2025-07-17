using Market.Entities;

namespace Market.Models.Products;

public class CreateProductResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public string Category { get; set; }

    public CreateProductResponseModel(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Price = product.Price;
        Amount = product.Amount;
        Category = product.Category.ToString();
    }
}