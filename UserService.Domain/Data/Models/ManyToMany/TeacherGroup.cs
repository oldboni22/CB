using UserService.Domain.Data.Models.RoleData;

namespace UserService.Domain.Data.Models.ManyToMany;

public class TeacherGroup : EntityBase
{
    public Guid TeacherId { get; init; }

    public TeacherRoleData TeacherData { get; init; } = null!;
    
    public Guid GroupId { get; init; }
}
