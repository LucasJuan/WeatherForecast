using WeatherGeocoding.Domain.Entities.External.OpenWeatherMap;


namespace WeatherGeocoding.Domain.Interface.External.OpenWeatherMap;

public interface IOpenWeatherMapService
{
    Task<WeatherForecastResult> GetWeatherForecastAsync(string address);
    Task<GeocodingResult> GeocodeAddressAsync(string address, string benchmark, string vintage);
}