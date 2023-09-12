using Microsoft.EntityFrameworkCore;

namespace NotificationService.Database;

public class DatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DatabaseContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var name = "mysql";
        var connectionString = _configuration.GetConnectionString(name);

        if (connectionString == null)
            throw new InvalidOperationException($"Configuration doesn't contain connection string with name '{name}'");
        
        optionsBuilder.UseMySQL(connectionString);
        
        base.OnConfiguring(optionsBuilder);
    }
}