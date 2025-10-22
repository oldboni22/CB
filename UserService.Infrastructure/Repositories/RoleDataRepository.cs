using Microsoft.EntityFrameworkCore;
using UserService.Domain.Context;
using UserService.Domain.Data.Models;
using UserService.Domain.Data.Models.RoleData;
using UserService.Domain.Interfaces.Repository;

namespace UserService.Infrastructure.Repositories;

public class RoleDataRepository(UserServiceDbContext context) : GenericRepository<RoleData>(context), IRoleDataRepository
{
    public async Task<StudentRoleData?> GetStudentDataAsync(User user, bool trackChanges = true)
    {
        var query = Context
            .Entry(user)
            .Reference(u => u.RoleData)
            .Query()
            .OfType<StudentRoleData>();
        
        query = trackChanges ? query : query.AsNoTracking();

        return await query.SingleOrDefaultAsync();
    }
    
    public async Task<TeacherRoleData?> GetTeacherDataAsync(User user, bool trackChanges = true)
    {
        var query = Context
            .Entry(user)
            .Reference(u => u.RoleData)
            .Query()
            .OfType<TeacherRoleData>();
        
        query = trackChanges ? query : query.AsNoTracking();

        return await query.SingleOrDefaultAsync();
    }
}
