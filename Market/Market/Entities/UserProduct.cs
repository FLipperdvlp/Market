using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Entities;

public class UserProduct
{
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    
    [ForeignKey("Product")]
    public Guid ProductId { get; set; }
    
    public User?  User { get; set; }
    public Product?  Product { get; set; }
}