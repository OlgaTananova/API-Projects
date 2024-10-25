using System;
using System.Text.Json;
using WeatherDataApp.Models;
using DotNetEnv;

namespace WeatherDataApp.Services;

public class WeatherService
{

    private readonly HttpClient _httpClient;


    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        
    }

    public async Task<WeatherInfo> GetWeatherDataAsync(string city, string apiKey)
    {
        var response = await _httpClient.GetAsync($"current.json?key={apiKey}&q={city}");
        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<WeatherInfo>(responseBody, options);

        }
        return null;
    }

}
