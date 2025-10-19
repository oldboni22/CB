using UserService.Domain.Data.Models.RoleData;

namespace UserService.Domain.Data.Models.Relations;

public class TeacherGroup : EntityBase
{
    public Guid TeacherDataId { get; init; }

    public TeacherRoleData TeacherData { get; init; } = null!;
    
    public Guid GroupId { get; init; }
}
