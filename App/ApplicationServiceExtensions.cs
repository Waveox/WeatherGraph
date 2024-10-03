using FluentValidation.AspNetCore;
using WeatherGraph.App.Services;
using WeatherGraph.App.Services.Implementations;
using WeatherGraph.App.Services.BackgroundServices;

namespace WeatherGraph.App;

public static class ApplicationServiceExtensions
{
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddScoped<IWeatherService, WeatherService>();
        builder.Services.AddHttpClient<IWeatherService, WeatherService>();
        builder.Services.AddHostedService<WeatherBackgroundService>();

        return builder;
    }
}