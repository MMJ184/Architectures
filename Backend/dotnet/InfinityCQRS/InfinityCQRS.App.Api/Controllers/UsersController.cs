using InfinityCQRS.App.CommandResults;
using InfinityCQRS.App.CommandResults.Users;
using InfinityCQRS.App.Commands.User;
using InfinityCQRS.App.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfinityCQRS.App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ApiBaseController
    {
        public UsersController(IMediator mediator) : base(mediator)
        { }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(UserRoles.SuperAdmin)]
        public async Task<ActionResult<ResponseBase<GetUsersResult>>> GetById(string id, CancellationToken cancellationToken)
            => GetResponse(await GetMediator(new GetUserByIdCommand { Id = id }));

        /// <summary>
        /// Get all users
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("users")]
        [Authorize(UserRoles.SuperAdmin)]
        public async Task<ActionResult<ResponseBase<List<GetUsersResult>>>> GetUsers([FromQuery] GetUsersCommand request, CancellationToken cancellationToken)
            => GetResponse(await GetMediator(request));

        /// <summary>
        /// Get users count
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("count")]
        [Authorize(UserRoles.SuperAdmin)]
        public async Task<ActionResult<ResponseBase<long>>> GetCount([FromQuery] GetUsersCountRequest request, CancellationToken cancellationToken)
            => GetResponse(await GetMediator(request));

        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ResponseBase<AddUserResult>>> Add(AddUserCommand request)
             => GetResponse(await GetMediator(request));

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ResponseBase<UpdateUserResult>>> Update(UpdateUserCommand request)
             => GetResponse(await GetMediator(request));


        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<ResponseBase<DeleteUserResult>>> Delete(DeleteUserCommand request)
             => GetResponse(await GetMediator(request));
    }
}
