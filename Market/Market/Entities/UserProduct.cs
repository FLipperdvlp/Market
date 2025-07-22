using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Entities;

public class UserProduct
{
    public Guid UserId { get; set; }
    
    public Guid ProductId { get; set; }
    
    public User?  User { get; set; }
    public Product?  Product { get; set; }
}