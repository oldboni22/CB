using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Internal;

namespace UserService.Domain.Context.Factories;

public class UserDbContextFactory : IDesignTimeDbContextFactory<UserServiceDbContext>
{
    public UserServiceDbContext CreateDbContext(string[] args)
    {
        //Set before creating migration and then clear
        var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=123456789;Database=UserDb";

        var optionsBuilder =  new DbContextOptionsBuilder();
        optionsBuilder.UseNpgsql(connectionString);
        
        return new UserServiceDbContext(optionsBuilder.Options);
    }
}