namespace WeatherGeocoding.Domain.Entities.External.OpenWeatherMap;
public class WeatherForecastResult: ErrorResponse
{
    public WeatherForecastResult() { }

    public WeatherForecastResult(string errorMessage) : base(errorMessage) { }
    public DailyForecast[] DailyForecasts { get; set; }
}

public class DailyForecast
{
    public DateTime Date { get; set; }
    public double TemperatureMax { get; set; }
    public double TemperatureMin { get; set; }
    public string Description { get; set; }
    public string WeatherIcon { get; set; }
}