namespace WeatherGeocoding.Domain.Entities.External.OpenWeatherMap
{
    public abstract class ErrorResponse
    {
        public ErrorResponse()
        {

        }

        public ErrorResponse(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

       public virtual string? ErrorMessage { get; set; }

       
    }
}