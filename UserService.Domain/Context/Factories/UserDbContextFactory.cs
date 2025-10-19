using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Internal;

namespace UserService.Domain.Context.Factories;

public class UserDbContextFactory : IDesignTimeDbContextFactory<UserServiceDbContext>
{
    public UserServiceDbContext CreateDbContext(string[] args)
    {
        //Set before creating migration and then clear
        var connectionString = ".!.";

        var optionsBuilder =  new DbContextOptionsBuilder();
        optionsBuilder.UseNpgsql(connectionString);
        
        return new UserServiceDbContext(optionsBuilder.Options);
    }
}