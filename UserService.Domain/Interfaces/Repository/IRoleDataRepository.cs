using UserService.Domain.Data.Models;
using UserService.Domain.Data.Models.RoleData;

namespace UserService.Domain.Interfaces.Repository;

public interface IRoleDataRepository : IGenericRepository<RoleData>
{
    Task<StudentRoleData?> GetStudentDataAsync(User user, bool trackChanges = true);

    Task<TeacherRoleData?> GetTeacherDataAsync(User user, bool trackChanges = true);
}
