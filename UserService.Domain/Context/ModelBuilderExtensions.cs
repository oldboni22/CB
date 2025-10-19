using Microsoft.EntityFrameworkCore;
using UserService.Domain.Data.Models;
using UserService.Domain.Data.Models.ManyToMany;
using UserService.Domain.Data.Models.RoleData;

namespace UserService.Domain.Context;

public static class ModelBuilderExtensions
{
    internal static ModelBuilder ConfigureRoleData(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<RoleData>()
            .HasDiscriminator<string>("RoleType")
            .HasValue<StudentRoleData>("Student")
            .HasValue<TeacherRoleData>("Teacher");

        return modelBuilder;
    }

    internal static ModelBuilder ConfigureUser(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<User>()
            .HasOne(u => u.RoleData)
            .WithOne()
            .HasForeignKey<User>(u => u.RoleDataId);

        modelBuilder
            .ConfigureTeacherData()
            .ConfigureStudentData();
        
        return modelBuilder;
    }

    private static ModelBuilder ConfigureStudentData(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<StudentRoleData>()
            .HasMany<StudentCourse>(data => data.StudentCourseRelations)
            .WithOne(relation => relation.StudentData)
            .HasForeignKey(relation => relation.StudentId);

        return modelBuilder;
    }
    
    private static ModelBuilder ConfigureTeacherData(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<TeacherRoleData>()
            .HasMany<TeacherGroup>(data => data.TeacherGroupRelations)
            .WithOne(relation => relation.TeacherData)
            .HasForeignKey(relation => relation.TeacherId);

        return modelBuilder;
    }
}
