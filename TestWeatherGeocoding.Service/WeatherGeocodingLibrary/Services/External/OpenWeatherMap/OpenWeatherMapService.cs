﻿using Microsoft.Extensions.Options;
using System.Text.Json;
using WeatherGeocoding.Domain.Entities.External.OpenWeatherMap;
using WeatherGeocoding.Domain.Interface.External.OpenWeatherMap;

namespace WeatherGeocoding.Domain.Services.External.OpenWeatherMap;

public class OpenWeatherMapService : IOpenWeatherMapService
{
    private readonly IOptions<WeatherGeocodingOptions> _options;
    private readonly HttpClient _httpClient;

    public OpenWeatherMapService(IOptions<WeatherGeocodingOptions> options, HttpClient customHttpClient)
    {
        _options = options;
        _httpClient = customHttpClient;
    }

    /// <summary>
    /// Gets the weather forecast based on the provided address.
    /// </summary>
    /// <param name="address">The address for which to retrieve the weather forecast.</param>
    /// <returns>The weather forecast result.</returns>
    public async Task<WeatherForecastResult> GetWeatherForecastAsync(string address)
    {

        var geoCodeResult = await GeocodeAddressAsync(address, _options.Value.Benchmark, _options.Value.Vintage);

        if (!string.IsNullOrEmpty(geoCodeResult.ErrorMessage))
            return new WeatherForecastResult(geoCodeResult.ErrorMessage);

        if (geoCodeResult is not null)
        {
            var longitude = geoCodeResult?.result.addressMatches.Select(x => x.coordinates.x).FirstOrDefault();
            var latitude = geoCodeResult?.result.addressMatches.Select(y => y.coordinates.y).FirstOrDefault();

            string url = $"{_options.Value.OpenWeatherURL}lat={latitude}&lon={longitude}&exclude=current,minutely,hourly&appid={_options.Value.ApiKey}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        var result = JsonSerializer.Deserialize<WeatherForecastResult>(await reader.ReadToEndAsync());
                        return result is not null ? result : new WeatherForecastResult($"Error to Deserialize Response API OpenWeatherMap");
                    }
                }
                else
                {
                    return new WeatherForecastResult($"Error to call API OpenWeatherMap: {(int)response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException ex)
            {
                return new WeatherForecastResult($"Error to call API OpenWeatherMap: {ex.Message}");
            }
        }
        else
        {
            return new WeatherForecastResult($"Address not found");
        }
    }

    /// <summary>
    /// Obtain location information based on address.
    /// </summary>
    /// <param name="address">The address to geocode.</param>
    /// <param name="benchmark">The benchmark for geocoding.</param>
    /// <param name="vintage">The vintage for geocoding.</param>
    /// <returns>The geocoding result.</returns>
    public async Task<GeocodingResult> GeocodeAddressAsync(string address, string benchmark, string vintage)
    {
        string apiUrl = $"{_options.Value.GeoCodingURL}address={Uri.EscapeDataString(address)}&benchmark={benchmark}&vintage={vintage}&format=json&";

        try
        {
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                using (Stream stream = await response.Content.ReadAsStreamAsync())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var result = JsonSerializer.Deserialize<GeocodingResult>(await reader.ReadToEndAsync());
                    return result is not null ? (result.result.addressMatches.Count <= 0 ? new GeocodingResult($"No results found for the specified address") : result)
                                              : new GeocodingResult($"Error to Deserialize Response API Geocoding");
                }
            }
            else
            {
                return new GeocodingResult($"Error to call API Geocoding: {(int)response.StatusCode} - {response.ReasonPhrase}");
            }
        }
        catch (HttpRequestException ex)
        {
            return new GeocodingResult($"Error to call API Geocoding: {ex.Message}");
        }
    }
}