using InfinityDapper.DTO.Request.Authentication;
using InfinityDapper.DTO.Response.Authentication;
using InfinityDapper.Repository.Abstract;
using InfinityDapper.Services.Abstract;
using InfinityDapper.Services.Common;
using Microsoft.Extensions.Configuration;

namespace InfinityDapper.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _config;

        public AccountService(IUserRepository repository,
                              IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public AuthenticationResponse Authenticate(LoginRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                    throw new Exception("Your Email Id or Password is incorrect!");

                var user = _repository.Login(request);

                if (user != null)
                {
                    if (user.IsDisabled)
                        throw new Exception("Your account is not approved yet. Please contact to admin.");
                    else
                    {
                        var jwtToken = CommonMethods.GenerateJwtToken(user.Id, user.Name, user.Email);
                        return new AuthenticationResponse() { Token = jwtToken };
                    }
                }
                else
                {
                    throw new Exception("Your Email Id or Password is incorrect!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Your process couldn't be complete at this time. Please try after some time.");
            }
        }
    }
}
