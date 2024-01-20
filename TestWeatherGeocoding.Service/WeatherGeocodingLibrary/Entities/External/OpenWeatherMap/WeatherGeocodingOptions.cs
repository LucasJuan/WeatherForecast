namespace WeatherGeocoding.Domain.Entities.External.OpenWeatherMap;

public class WeatherGeocodingOptions
{
    public string ApiKey { get; set; }
    public string Benchmark {  get; set; }
    public string Vintage { get; set; }
    public string OpenWeatherURL { get; set; }
    public string GeoCodingURL { get; set; }
}