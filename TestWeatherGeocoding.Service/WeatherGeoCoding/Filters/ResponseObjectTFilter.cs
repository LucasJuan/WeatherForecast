using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WeatherGeocoding.API.Model;

namespace WeatherGeocoding.API.Filters
{
    public class ResponseObjectTFilter : ActionFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;

            VerifyContextStatusCode(result);

            await next();
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();

            var result = context.Result as ObjectResult;

            VerifyContextStatusCode(result);
        }

        #region Private
        private static void VerifyContextStatusCode(ObjectResult? result)
        {
            if (result != null)
            {
                if (result.StatusCode >= 200 && result.StatusCode <= 299)
                {
                    result.Value = VO(result.Value!);
                }
                else
                {
                    CheckErrorValueResult(result);
                }
            }
        }


        private static void CheckErrorValueResult(ObjectResult? result)
        {
            Type resultType = result!.Value!.GetType();

            var errorMessageProperty = resultType.GetProperty("ErrorMessage");

            if (result!.Value!.GetType() == typeof(ValidationProblemDetails))
            {
                var obj = result.Value as ValidationProblemDetails;
                var res = string.Join("; ", obj!.Errors.SelectMany(el => el.Value.Select(el => el)));
                result.Value = VOError(res);
            }
            else if (result.Value is ProblemDetails problemDetails)
            {
                result.Value = VOError(problemDetails.Detail ?? problemDetails.Title ?? "Error");
            }
            else if (result.Value is IDictionary<string, object> dictionary)
            {
                var errorMessages = new List<string>();

                foreach (var key in dictionary)
                {
                    if (key.Value is IEnumerable<object> array)
                    {
                        var arrayErrors = array.Select(el => el?.ToString());
                        errorMessages.AddRange(arrayErrors!);
                    }
                    else
                    {
                        errorMessages.Add(key.Value?.ToString() ?? "");
                    }
                }
                var errorMessage = string.Join("; ", errorMessages);
                result.Value = VOError(errorMessage);
            }
            else if (errorMessageProperty != null)
            {
                object errorMessageValue = errorMessageProperty.GetValue(result.Value)!;

                result.Value = VOError(errorMessageValue?.ToString() ?? "Value is null");
            }
            else
            {
                result.Value = VOError(result.Value?.ToString() ?? "Value is null");
            }
        }

        private static ObjectResponse<object> VO(object data, object? dataId = null)
        {
            if (data.GetType() != typeof(string))
            {
                return new ObjectResponse<object>
                {
                    Data = data,
                    Success = true,
                    Message = string.Empty
                };
            }
            else
            {
                return new ObjectResponse<object>
                {
                    Data = dataId != null ? dataId : default,
                    Success = true,
                    Message = data.ToString()
                };
            }
        }

        private static ObjectResponse<object> VOError(object data)
        {
            if (data.GetType() != typeof(string))
            {
                return new ObjectResponse<object>
                {
                    Data = data,
                    Success = false,
                    Message = ""
                };
            }
            else
            {
                var message = data.ToString();
             
                return new ObjectResponse<object>
                {
                    Data = null!,
                    Success = false,
                    Message = message
                };
            }
        }

        #endregion
    }
}