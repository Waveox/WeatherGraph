namespace WeatherGraph.App.Services;
public interface IWeatherService
{
    Task AddWeatherDataAsync(string country, string city);
}
