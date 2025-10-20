using Microsoft.EntityFrameworkCore;
using UserService.Domain.Data.Models;
using UserService.Domain.Data.Models.Relations;
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

        modelBuilder
            .ConfigureTeacherData()
            .ConfigureStudentData();
        
        return modelBuilder;
    }

    internal static ModelBuilder ConfigureUser(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<User>()
            .HasOne(u => u.RoleData)
            .WithOne()
            .HasForeignKey<User>(u => u.RoleDataId);
        
        return modelBuilder;
    }

    private static ModelBuilder ConfigureStudentData(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<StudentRoleData>()
            .HasMany<StudentCourse>(data => data.StudentCourseRelations)
            .WithOne(relation => relation.StudentData)
            .HasForeignKey(relation => relation.StudentDataId);

        return modelBuilder;
    }
    
    private static ModelBuilder ConfigureTeacherData(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<TeacherRoleData>()
            .HasMany<TeacherGroup>(data => data.TeacherGroupRelations)
            .WithOne(relation => relation.TeacherData)
            .HasForeignKey(relation => relation.TeacherDataId);

        modelBuilder
            .Entity<TeacherRoleData>()
            .HasMany<TeacherCourse>(data => data.TeacherCourseRelations)
            .WithOne(relation => relation.TeacherData)
            .HasForeignKey(relation => relation.TeacherDataId);
        
        return modelBuilder;
    }
}
