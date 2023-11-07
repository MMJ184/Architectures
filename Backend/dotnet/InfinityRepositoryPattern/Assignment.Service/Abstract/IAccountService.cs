using Assignment.Service.Common;
using Assignment.Service.DTO.Account;

namespace Assignment.Service.Abstract
{
    public  interface IAccountService
    {
        Task<ResponseBase<AuthenticationResponse>> Authentication(AuthenticationRequest request);
    }
}
