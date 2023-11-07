using InfinityCQRS.App.CommandResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InfinityCQRS.App.Api.Controllers
{
    /// <summary>
    /// Base Class for APIs
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        public ApiBaseController(IMediator mediator)
            => (Mediator) = (mediator);

        /// <summary>
        /// Mediator to handle request
        /// </summary>
        protected IMediator Mediator { get; }

        /// <summary>
        /// Get Response of a Request
        /// </summary>
        /// <typeparam name="TResult">Type of Result</typeparam>
        /// <param name="request">request</param>
        /// <returns>Response</returns>
        protected virtual async Task<TResult> GetMediator<TResult>(IRequest<TResult> request)
            => await Mediator.Send(request);

        /// <summary>
        /// Get Response based on on Result
        /// </summary>
        /// <typeparam name="TResult">BaseResult</typeparam>
        /// <param name="result">result to convert to response</param>
        /// <returns>Response</returns>
        protected ActionResult<TResult> GetResponse<TResult>(TResult result) where TResult : ResponseBaseModel
        {
            return result.ResponseStatusCode switch
            {
                (HttpStatusCode.OK) => Ok(result),
                (HttpStatusCode.NotFound) => NotFound(result),
                (HttpStatusCode.BadRequest) => BadRequest(result),
                _ => StatusCode((int)result.ResponseStatusCode, result)
            };
        }
    }
}
