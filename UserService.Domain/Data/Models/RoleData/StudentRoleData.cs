using UserService.Domain.Data.Models.ManyToMany;

namespace UserService.Domain.Data.Models.RoleData;

public class StudentRoleData : RoleData
{
    public Guid GroupId { get; set; }

    public List<StudentCourse> StudentCourseRelations { get; set; } = [];
}
