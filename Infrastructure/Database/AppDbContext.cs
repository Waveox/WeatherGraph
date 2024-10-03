using WeatherGraph.App.Models;
using WeatherGraph.Infrastructure.Database.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace WeatherGraph.Infrastructure.Database;

public sealed class AppDbContext : DbContext
{
    public DbSet<WeatherData> WeatherData { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.AddInterceptors(new TimestampsInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}