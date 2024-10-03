using System.Text.Json;
using WeatherGraph.App.Models;
using WeatherGraph.Infrastructure.Database;

namespace WeatherGraph.App.Services.Implementations;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly AppDbContext _dbContext;
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(HttpClient httpClient, ILogger<WeatherService> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _httpClient = httpClient;
        _dbContext = dbContext;
    }

    public async Task AddWeatherDataAsync(string country, string city)
    {
        var apiKey = "bd2fb449304cf38ec0b7fb59710368ec";
        var requestUri = $"https://api.openweathermap.org/data/2.5/weather?q={city},{country}&appid={apiKey}&units=metric";

        var response = await _httpClient.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"Unable to fetach data from weather API for country:{country} city:{city}");
            return;
        }

        try
        {
            var responseData = await response.Content.ReadAsStringAsync();
            var weatherApiResponse = JsonSerializer.Deserialize<WeatherApiResponse>(responseData);

            var weatherData = new WeatherData
            {
                Country = country,
                City = city,
                Temperature = weatherApiResponse.Main.Temp,
                MinTemperature = weatherApiResponse.Main.TempMin,
                MaxTemperature = weatherApiResponse.Main.TempMax
            };

            _dbContext.WeatherData.Add(weatherData);
            await _dbContext.SaveChangesAsync();
        }
        catch(Exception e) 
        { 
            _logger.LogError($"Unable to save weather data for country:{country} city:{city}", e.Message);
        }
    }
}