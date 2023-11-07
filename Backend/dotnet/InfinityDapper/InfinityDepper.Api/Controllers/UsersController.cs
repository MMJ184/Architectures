using InfinityDapper.DTO.Request.User;
using InfinityDapper.DTO.Response.Users;
using InfinityDapper.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace InfinityDapper.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService service,
                              ILogger<UsersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Add/Update 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("add-update")]
        public ActionResult<UserResponse> AddUpdate(AddUpdateUserRequest request)
            => _service.AddUpdate(request);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public ActionResult<bool> Delete([FromQuery] DeleteUserRequest request)
            => _service.Delete(request);

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("get-by-id")]
        public ActionResult<UserResponse> GetById([FromQuery] GetUserByIdRequest request)
            => _service.GetById(request);

        /// <summary>
        /// Get All 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult<List<UserResponse>> GetAsync([FromQuery] GetUsersRequest request)
            => _service.GetAsync(request);

        /// <summary>
        /// Get count
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("count")]
        public ActionResult<double> GetCountAsync([FromQuery] GetUsersRequest request)
            => _service.GetCountAsync(request);
    }
}
