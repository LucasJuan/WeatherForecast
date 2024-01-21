using System.Text.Json.Serialization;

namespace WeatherGeocoding.Domain.Entities.External.OpenWeatherMap;
public class WeatherForecastResult: ErrorResponse
{
    public WeatherForecastResult() { }

    public WeatherForecastResult(string errorMessage) : base(errorMessage) { }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    [JsonPropertyName("generationtime_ms")]
    public double GenerationTimeMs { get; set; }
    public int UtcOffsetSeconds { get; set; }
    public string Timezone { get; set; }
    [JsonPropertyName("timezone_abbreviation")]
    public string TimezoneAbbreviation { get; set; }
    public double Elevation { get; set; }
    [JsonPropertyName("current_units")]
    public CurrentUnits CurrentUnits { get; set; }
    public CurrentWeather Current { get; set; }
    [JsonPropertyName("daily_units")]
    public DailyUnits DailyUnits { get; set; }
    public DailyWeather Daily { get; set; }
}

public class CurrentUnits
{
    public string Time { get; set; }
    public string Interval { get; set; }
    public string Temperature2m { get; set; }
}

public class CurrentWeather
{
    public string Time { get; set; }
    public int Interval { get; set; }
    public double Temperature2m { get; set; }
}

public class DailyUnits
{
    public string Time { get; set; }
    [JsonPropertyName("weather_code")]
    public string WeatherCode { get; set; }
}

public class DailyWeather
{
    public List<string> Time { get; set; }
    [JsonPropertyName("weather_code")]
    public List<int> WeatherCode { get; set; }
}