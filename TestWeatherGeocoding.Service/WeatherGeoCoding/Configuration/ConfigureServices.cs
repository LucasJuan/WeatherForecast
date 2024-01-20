
using WeatherGeocoding.Domain.Entities.External.OpenWeatherMap;
using WeatherGeocoding.Domain.Interface.External.OpenWeatherMap;
using WeatherGeocoding.Domain.Services.External.OpenWeatherMap;

namespace WeatherGeoCoding.Configuration;

public static class ConfigureServices
{
    public static void Register(this IServiceCollection services, ConfigurationManager config)
    {
        services.Configure<WeatherGeocodingOptions>(config.GetSection("OpenWeatherMap"));
        services.AddHttpClient<OpenWeatherMapService>();
        services.AddSingleton<IOpenWeatherMapService, OpenWeatherMapService>();
    }
    public static void RegisterCors(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });
    }
}
