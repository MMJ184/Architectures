using InfinityDapper.DTO.Request.Authentication;
using InfinityDapper.DTO.Response.Authentication;

namespace InfinityDapper.Services.Abstract
{
    public interface IAccountService
    {
        AuthenticationResponse Authenticate(LoginRequest request);
    }
}
