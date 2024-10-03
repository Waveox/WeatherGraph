using WeatherGraph.App;
using WeatherGraph.Infrastructure.Database;
using InertiaCore.Extensions;

namespace WeatherGraph;

public static class AppServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddInertia(options =>
        {
            options.RootView = "/Web/Views/App.cshtml";
            options.SsrEnabled = false;
        });

        builder.Services.AddViteHelper(options =>
        {
            options.PublicDirectory = "wwwroot";
            options.BuildDirectory = "dist";
            options.HotFile = "hot";
            options.ManifestFilename = "manifest.json";
        });

        builder.Services.AddControllersWithViews()
            .AddSessionStateTempDataProvider();

        builder.Services.AddSession();

        builder.AddApplicationServices();
        builder.AddDataServices();
    }
}