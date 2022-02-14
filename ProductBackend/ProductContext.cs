using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProductBackend;

public class ProductContext : DbContext
{
    private string ConnectionString { get; set; }

    public DbSet<Models.Product> Products { get; set; } = null!;


    public ProductContext()
    {
        //ConnectionString = "Host=localhost;Username=somebody;Password=somepwd;"

        ConnectionString = Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_NexxtRdb") ?? "";
        if (string.IsNullOrWhiteSpace(ConnectionString))
            throw new Exception("Connection string is empty");
        var contextName = this.GetType().Name;
        var databaseName = contextName.Remove(contextName.Length - "Context".Length);
        ConnectionString += ";Database=" + databaseName;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options
            .UseNpgsql(ConnectionString);

    }
}