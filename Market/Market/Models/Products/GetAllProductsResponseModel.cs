using Market.Entities;

namespace Market.Models.Products;

public class GetAllProductsResponseModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public string Category { get; set; }

    public GetAllProductsResponseModel(Product product)
    {
        Id = product.Id;
        CreatedAt = product.CreatedAt;
        Name = product.Name;
        Price = product.Price;
        Amount = product.Amount;
        Category = product.Category.ToString();
    }
}