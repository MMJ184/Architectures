using Assignment.Database.Entities;
using Assignment.Repository.Abstract;
using Assignment.Repository.Common;
using Assignment.Service.Abstract;
using Assignment.Service.Common;
using Assignment.Service.DTO.Account;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using BC = BCrypt.Net.BCrypt;

namespace Assignment.Service.Concrete
{
    public class AccontService : IAccountService
    {
        private readonly IConfiguration _config;
        public readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        //AppSettingsConfiguration AppSettingsConfiguration = new AppSettingsConfiguration();
        public AccontService(IUserRepository userRepository,
            IConfiguration config,
                             IMapper mapper)
        {
            _userRepository = userRepository;
            //AppSettingsConfiguration.Secret = _config.GetSection("AppSettings:Secret").Value;
            //AppSettingsConfiguration.RefreshTokenTTL = Convert.ToInt32(_config.GetSection("AppSettings:RefreshTokenTTL").Value);
            _mapper = mapper;
        }

        public async Task<ResponseBase<AuthenticationResponse>> Authentication(AuthenticationRequest request)
        {
            try
            {
                var result = new AuthenticationResponse();
                var user = await _userRepository.GetUserByEmailAsync(request.Email);
                if (user == null || !BC.Verify(request.Password, user.PasswordHash))
                    throw new InvalidOperationException("Email Id / Password is wrong.");
                else
                {
                    //var jwtToken = GenerateJwtToken(user);
                    var authenticateResponse = _mapper.Map<AuthenticationResponse>(user);

                    result = authenticateResponse;
                }
                return new ResponseBase<AuthenticationResponse>(result);
            }
            catch (Exception ex)
            {
                var result = new ResponseBase<AuthenticationResponse>(null) { ResponseStatusCode = System.Net.HttpStatusCode.Unauthorized };
                result.AddExceptionLog(ex);
                return result;
            }
        }

        //private string GenerateJwtToken(Users user)
        //{
        //    //var tokenHandler = new JwtSecurityTokenHandler();
        //    //var key = Encoding.ASCII.GetBytes(AppSettingsConfiguration.Secret);
        //    //var tokenDescriptor = new SecurityTokenDescriptor
        //    //{
        //    //    Subject = new ClaimsIdentity(new[] { new Claim("UserId", user.Id.ToString()),
        //    //                                         new Claim("Role", string.Join(", ", user.UserToRoles.Select(x => x.Roles.Name).ToList())) }),
        //    //    Expires = DateTime.UtcNow.AddHours(1),
        //    //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    //};
        //    //var token = tokenHandler.CreateToken(tokenDescriptor);
        //    //return tokenHandler.WriteToken(token);
        //    return null;
        //}
    }
}
