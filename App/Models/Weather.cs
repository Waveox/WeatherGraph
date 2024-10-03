using WeatherGraph.App.Models.Base;

namespace WeatherGraph.App.Models;

public sealed class WeatherData : ITimestamped, IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public double Temperature { get; set; }
    public double MinTemperature { get; set; }
    public double MaxTemperature { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
