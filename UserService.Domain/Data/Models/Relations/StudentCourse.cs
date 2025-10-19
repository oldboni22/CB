using UserService.Domain.Data.Models.RoleData;

namespace UserService.Domain.Data.Models.Relations;

public class StudentCourse : EntityBase
{
    public Guid StudentDataId { get; init; }

    public StudentRoleData StudentData { get; init; } = null!;
    
    public Guid CourseId { get; init; }
}
