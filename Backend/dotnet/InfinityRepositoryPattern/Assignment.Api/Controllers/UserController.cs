using Assignment.Service.Abstract;
using Assignment.Service.Common;
using Assignment.Service.DTO.User;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService,
                              IMapper mapper) : base(mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]

        public async Task<ResponseBase<UserResponse>> Add (AddUserRequest request , CancellationToken cancellationToken)
            => await _userService.Add (request , cancellationToken);

        [HttpPut]

        public async Task<ResponseBase<UserResponse>> Update (UpdateUserRequest request , CancellationToken cancellationToken)
            => await _userService.Update (request , cancellationToken);

        [HttpDelete("{id}")]
        public async Task<ResponseBase<bool>> Delete (int id)
            => await _userService.Delete (id);

        [HttpGet("id")]

        public async Task<ResponseBase<UserResponse>> GetById (int id)
            => await _userService.GetById(id);

        [HttpGet("count")]
        public async Task<ResponseBase<long>> GetCount ()
            => await _userService.GetCount();

        [HttpGet]
        public async Task<ResponseBase<List<UserResponse>>> GetAll([FromQuery] GetUserRequest request) 
            => await _userService.GetAsync(request);


    }
}
