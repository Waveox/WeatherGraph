using Microsoft.AspNetCore.Mvc;
using WeatherGraph.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace WeatherGraph.Web.Controllers;

public sealed class WeatherController(AppDbContext context, ILogger<WeatherController> logger) : BaseController
{

    [HttpGet]
    public async Task<IActionResult> GetWeatherData()
    {
        var weatherData = await context.WeatherData
            .OrderByDescending(w => w.Updated)
            .Take(100)
            .ToListAsync();

        return Ok(weatherData);
    }
}