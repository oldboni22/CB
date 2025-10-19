using UserService.Domain.Data.Models.Relations;

namespace UserService.Domain.Data.Models.RoleData;

public class TeacherRoleData : RoleData
{
    public List<TeacherGroup> TeacherGroupRelations { get; init; } = [];
    
    public List<TeacherCourse> TeacherCourseRelations { get; init; } = [];
}
