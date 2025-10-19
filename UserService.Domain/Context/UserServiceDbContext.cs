using Microsoft.EntityFrameworkCore;
using UserService.Domain.Data.Models;
using UserService.Domain.Data.Models.Relations;
using UserService.Domain.Data.Models.RoleData;

namespace UserService.Domain.Context;

public class UserServiceDbContext(DbContextOptions options) : DbContext(options)
{
    internal DbSet<User> Users { get; set; }
    
    internal DbSet<RoleData> UserRoleData { get; set; }
    
    internal DbSet<StudentCourse>  StudentCourseRelations { get; set; }
    
    internal DbSet<TeacherGroup> TeacherGroupRelations { get; set; }
    
    internal DbSet<TeacherCourse> TeacherCourseRelations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ConfigureRoleData()
            .ConfigureUser();
    }
}
