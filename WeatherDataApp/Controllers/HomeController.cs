using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WeatherDataApp.Models;
using WeatherDataApp.Services;

namespace WeatherDataApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly WeatherService _weatherService;

    public HomeController(ILogger<HomeController> logger, WeatherService weatherService)
    {
        _logger = logger;
        _weatherService = weatherService;
    }

    public async Task<IActionResult> Index()
    {
        var cities = new List<string> { "London", "New York", "Tokyo", "92604" };
        List<WeatherInfo> weatherdata = new List<WeatherInfo>();
        string apiKey = Environment.GetEnvironmentVariable("API_KEY");

        foreach (var city in cities)
        {
            var data = await _weatherService.GetWeatherDataAsync(city, apiKey);
            if (data != null)
            {
                weatherdata.Add(data);
            }
        }

        return View(weatherdata);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
