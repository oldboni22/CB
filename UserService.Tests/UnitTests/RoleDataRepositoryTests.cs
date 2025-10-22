using AutoFixture;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using UserService.Domain.Context;
using UserService.Domain.Data.Enums;
using UserService.Domain.Data.Models;
using UserService.Domain.Data.Models.RoleData;
using UserService.Infrastructure.Repositories;

namespace UserService.Tests.UnitTests;

public class RoleDataRepositoryTests : IDisposable, IAsyncDisposable
{
    private const string SqLiteInMemoryConnectionString = "DataSource=:memory:";
    
    private readonly IFixture _fixture;
    
    private readonly UserServiceDbContext  _context;

    private readonly RoleDataRepository _sut;
    
    private readonly SqliteConnection _sqliteConnection;
    
    public RoleDataRepositoryTests()
    {
        _fixture = new Fixture();

        _sqliteConnection =  new SqliteConnection(SqLiteInMemoryConnectionString);
        _sqliteConnection.Open();
        
        var dbContextOptionsBuilder = new DbContextOptionsBuilder();
        dbContextOptionsBuilder.UseSqlite(_sqliteConnection);
        
        _context = new UserServiceDbContext(dbContextOptionsBuilder.Options);

        _sut = new RoleDataRepository(_context);
            
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task GetStudentDataAsync_UserExistsAndIsStudent_ReturnsData_WithCorrectGroupId()
    {
        //Arrange
        var groupId = Guid.NewGuid();
        
        var roleData = _fixture
            .Build<StudentRoleData>()
            .With(data => data.GroupId, groupId)
            .With(data => data.StudentCourseRelations, [])
            .Create();
        
        var user = _fixture
            .Build<User>()
            .With(u => u.Role, UserRole.Student)
            .With(u => u.RoleData, roleData)
            .Create();
        
        await SeedData(new List<User> { user });
        
        //Act
        var result = await _sut.GetStudentDataAsync(user);
        
        //Assert
        result.ShouldNotBeNull();
        result.GroupId.ShouldBe(groupId);
    }
    
    [Fact]
    public async Task GetStudentDataAsync_UserExistsAndNotStudent_ReturnsNull()
    {
        //Arrange
        var user = _fixture
            .Build<User>()
            .With(u => u.Role, UserRole.Student)
            .With(u => u.RoleData, new TeacherRoleData())
            .Create();
        
        await SeedData(new List<User> { user });
        
        //Act
        var result = await _sut.GetStudentDataAsync(user);
        
        //Assert
        result.ShouldBeNull();
    }
    
    [Fact]
    public async Task GetTeacherDataAsync_UserExistsAndIsTeacher_ReturnsData()
    {
        //Arrange
        var user = _fixture
            .Build<User>()
            .With(u => u.Role, UserRole.Student)
            .With(u => u.RoleData, new TeacherRoleData())
            .Create();
        
        await SeedData(new List<User> { user });
        
        //Act
        var result = await _sut.GetTeacherDataAsync(user);
        
        //Assert
        result.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task GetTeacherDataAsync_UserExistsAndNotTeacher_ReturnsNull()
    {
        //Arrange
        var user = _fixture
            .Build<User>()
            .With(u => u.Role, UserRole.Student)
            .With(u => u.RoleData, new StudentRoleData())
            .Create();
        
        await SeedData(new List<User> { user });
        
        //Act
        var result = await _sut.GetTeacherDataAsync(user);
        
        //Assert
        result.ShouldBeNull();
    }
    
    private async Task SeedData<T>(IEnumerable<T> items) where T : EntityBase
    {
        _context.Set<T>().AddRange(items);
        
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        
        _context.Dispose();
        _sqliteConnection.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        
        await _context.DisposeAsync();
        await _sqliteConnection.DisposeAsync();
    }
}