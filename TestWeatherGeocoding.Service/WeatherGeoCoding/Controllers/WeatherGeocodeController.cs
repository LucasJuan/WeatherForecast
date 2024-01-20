using Microsoft.AspNetCore.Mvc;
using WeatherGeocoding.Domain.Entities.External.OpenWeatherMap;
using WeatherGeocoding.Domain.Interface.External.OpenWeatherMap;

namespace WeatherGeoCoding.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherGeocodeController : ControllerBase
{
    private readonly ILogger<WeatherGeocodeController> _logger;
    private readonly IOpenWeatherMapService _openWeatherMapService;

    public WeatherGeocodeController(ILogger<WeatherGeocodeController> logger, IOpenWeatherMapService openWeatherMapService)
    {
        _logger = logger;
        _openWeatherMapService = openWeatherMapService;
    }

    [HttpGet("GetWeatherForecast")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<WeatherForecastResult>> GetWeatherForecastAsync(string address)
    {
        var result = await _openWeatherMapService.GetWeatherForecastAsync(address);
        return result.ErrorMessage is not null ? NotFound(new WeatherForecastResult(string.Format(result!.ErrorMessage))) : Ok(result);
    }
}