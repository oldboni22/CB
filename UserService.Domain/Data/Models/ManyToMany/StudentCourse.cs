using UserService.Domain.Data.Models.RoleData;

namespace UserService.Domain.Data.Models.ManyToMany;

public class StudentCourse : EntityBase
{
    public Guid StudentId { get; init; }

    public StudentRoleData StudentData { get; init; } = null!;
    
    public Guid CourseId { get; init; }
}