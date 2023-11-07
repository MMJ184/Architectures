using InfinityDapper.DTO.Request.Authentication;
using InfinityDapper.DTO.Response.Authentication;
using InfinityDapper.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace InfinityDapper.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAccountService service,
                              ILogger<AuthController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("authentication")]
        public ActionResult<AuthenticationResponse> Authenticate(LoginRequest request)
            => _service.Authenticate(request);

    }
}