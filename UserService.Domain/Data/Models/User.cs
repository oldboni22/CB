using System.ComponentModel.DataAnnotations;
using UserService.Domain.Data.Enums;

namespace UserService.Domain.Data.Models;

public class User : EntityBase
{
    public required string Name { get; set; }
    
    public UserRole Role { get; init; }
    
    public Guid RoleDataId { get; set; }

    public RoleData.RoleData RoleData { get; set; } = null!;
}   
