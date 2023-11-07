using InfinityCQRS.App.CommandResults;
using System.Net;

namespace InfinityCQRS.App.Api.MiddleWares
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;

        //internal TelemetryClient _telemetryClient;

        //public ExceptionMiddleware(RequestDelegate next, IConfiguration configuration)
        //{
        //    _telemetryClient = new TelemetryClient(new Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration
        //    {
        //        InstrumentationKey = configuration["AppInsightsKey"]
        //    });

        //    // failsafe
        //    _telemetryClient.InstrumentationKey = configuration["AppInsightsKey"];

        //    _next = next;
        //}

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
               // _telemetryClient.TrackException(ex);

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(new ResponseBaseModel
            {
                ResponseStatusCode = (HttpStatusCode)(context?.Response?.StatusCode),
                Message = exception.ToString()
            }.ToString());
        }
    }
}
