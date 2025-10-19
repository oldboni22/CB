using UserService.Domain.Data.Models.Relations;

namespace UserService.Domain.Data.Models.RoleData;

public class StudentRoleData : RoleData
{
    public Guid GroupId { get; set; }

    public List<StudentCourse> StudentCourseRelations { get; init; } = [];
}
