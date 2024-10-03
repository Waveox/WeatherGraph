using Microsoft.EntityFrameworkCore;
using WeatherGraph.App.Models;

namespace WeatherGraph.Infrastructure.Database;

public class DatabaseManager : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<DatabaseManager> _logger;
    private readonly IServiceScope _scope;

    public DatabaseManager(IServiceProvider provider)
    {
        var scope = provider.CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        _env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
        _logger = scope.ServiceProvider.GetRequiredService<ILogger<DatabaseManager>>();
        _scope = scope;
    }

    public void Seed()
    {
        if (_env.IsDevelopment())
        {
           if (!_context.WeatherData.Any())
            {
                _context.WeatherData.Add(new WeatherData
                {
                    Id = Guid.NewGuid(),
                    Country = "LV",
                    City = "Riga",
                    Temperature = 20.5,
                    MinTemperature = 15.0,
                    MaxTemperature = 25.0,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow
                });

                _context.SaveChanges();
            }
        }

        _logger.LogInformation("Seeding complete");
    }

    public void Migrate()
    {
        _context.Database.Migrate();
    }

    public void Dispose()
    {
        _context.Dispose();
        _scope.Dispose();
    }
}