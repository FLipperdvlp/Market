using System.ComponentModel.DataAnnotations;
using ShopP21.Enums;

namespace ShopP21.Models.Products;

public class CreateProductRequestModel
{
    [Required, MaxLength(32)]
    public required string Name { get; set; }
    
    [Required, Range(0, int.MaxValue)]
    public decimal Price { get; set; }
    
    [Required, Range(0, int.MaxValue)]
    public int Amount { get; set; }
    
    [Required]
    public ProductCategory Category { get; set; }
}