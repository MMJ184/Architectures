using InfinityCQRS.App.CommandResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cycle.App.Api.Helpers
{
    public class BadRequestFormatAttribute : IAsyncAlwaysRunResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                try
                {
                    var baseResult = new ResponseBaseModel
                    {
                        ResponseStatusCode = System.Net.HttpStatusCode.BadRequest,
                        Errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage)).ToList()
                    };
                    context.Result = new BadRequestObjectResult(baseResult);
                }
                catch { }
            }
            await next();
        }
    }


}
