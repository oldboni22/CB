using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Data.Models;

public abstract class EntityBase
{
    [Key]
    public Guid Id { get; init; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}
