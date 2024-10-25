using System;

namespace WeatherDataApp.Models;

public class WeatherInfo
{
    public Location Location { get; set; }
    public Current Current { get; set; }
    
}

public class Location {
    public string Name { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
}

public class Current {
    public double Temp_C { get; set; }
    public double Temp_F { get; set; }
    public int Humidity { get; set; }
    public Condition Condition { get; set; }
}

public class Condition {
    public string Text { get; set; }
    public string Icon { get; set; }
}