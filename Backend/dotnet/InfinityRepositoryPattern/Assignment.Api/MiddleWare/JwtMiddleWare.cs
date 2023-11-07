using Assignment.Repository.Abstract;
using Assignment.Repository.Common;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Assignment.Api.MiddleWare
{
    public class JwtMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;
        AppSettingsConfiguration AppSettingsConfiguration = new AppSettingsConfiguration();

        public JwtMiddleWare(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
            AppSettingsConfiguration.Secret = _config.GetSection("AppSettings:Secret").Value;
            AppSettingsConfiguration.RefreshTokenTTL = Convert.ToInt32(_config.GetSection("AppSettings:RefreshTokenTTL").Value);
        }

        public async Task Invoke(HttpContext context, IUserRepository userRepository)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await AttachUserToContext(context, token, userRepository);

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, string token, IUserRepository userRepository)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AppSettingsConfiguration.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "UserId").Value);

                // attach user to context on successful jwt validation
                context.Items["User"] = await userRepository.GetUserByUserIdAsync(userId);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
