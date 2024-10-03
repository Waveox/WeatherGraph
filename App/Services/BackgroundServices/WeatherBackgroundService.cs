namespace WeatherGraph.App.Services.BackgroundServices;

public class WeatherBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly string[] _locations;

    public WeatherBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;

        _locations =
        [
            "LV,Riga",
            "SE,Malmo",
            "ES,Madrid"
        ];
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var weatherService = scope.ServiceProvider.GetRequiredService<IWeatherService>();

                foreach (var location in _locations)
                {
                    var locationParts = location.Split(',');
                    var country = locationParts[0];
                    var city = locationParts[1];

                    await weatherService.AddWeatherDataAsync(country, city);
                }
            }
            
            await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
        }
    }
}