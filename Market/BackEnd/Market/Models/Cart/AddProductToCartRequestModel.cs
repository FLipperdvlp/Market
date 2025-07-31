using System.ComponentModel.DataAnnotations;

namespace ShopP21.Models.Cart;

public class AddProductToCartRequestModel
{
    [Required]
    public Guid ProductId { get; set; }
}