using UserService.Domain.Data.Models.ManyToMany;

namespace UserService.Domain.Data.Models.RoleData;

public class TeacherRoleData : RoleData
{
    public List<TeacherGroup> TeacherGroupRelations { get; set; } = [];
}
