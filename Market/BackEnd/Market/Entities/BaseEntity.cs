using System.ComponentModel.DataAnnotations;

namespace ShopP21.Entities;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Soft Delete
    public bool IsDeleted { get; set; } = false;
}