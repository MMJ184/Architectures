using Assignment.Service.Abstract;
using Assignment.Service.Common;
using Assignment.Service.DTO.Account;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace Assignment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AdminController : ApiControllerBase
    {

         private readonly IAccountService _accountService;

        public AdminController(IAccountService accountService,
                               IMapper mapper) : base(mapper)
        {
            _accountService = accountService;
        }

        [HttpPost("authenticate")]

        public async Task<ActionResult<ResponseBase<AuthenticationResponse>>> Authentication([FromBody] AuthenticationRequest request)
        => await _accountService.Authentication(request);

    }
}
