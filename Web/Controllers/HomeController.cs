using InertiaCore;
using Microsoft.AspNetCore.Mvc;

namespace WeatherGraph.Web.Controllers;

public class HomeController : BaseController
{
    public IActionResult Index()
    {
        return Inertia.Render("Index");
    }
}