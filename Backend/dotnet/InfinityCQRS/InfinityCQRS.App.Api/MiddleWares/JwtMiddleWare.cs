using InfinityCQRS.Backend.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using InfinityCQRS.App.Repository.Common;

namespace InfinityCQRS.App.Api.MiddleWares
{
    /// <summary>
    /// Middleware to handle authorization pipeline for apis
    /// </summary>
    public class JwtMiddleWare
    {
        private const string Auth = "Authorization";
        private readonly RequestDelegate _next;

        /// <summary>
        /// Method to initialize request delegate
        /// </summary>
        /// <param name="next"></param>
        public JwtMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Method to parse and manage the http context in use
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                //if (!httpContext.Request.Path.StartsWithSegments("/api/PaymentIntegration"))
                //{
                    var userRepo = (IBaseRepository<ApplicationUser>)httpContext.RequestServices.GetService(typeof(IBaseRepository<ApplicationUser>));
                    if (httpContext.Request.Headers.TryGetValue(Auth, out var authValue))
                    {

                        var handler = new JwtSecurityTokenHandler();
                        SecurityToken jsonToken = null;
                        try
                        {
                            jsonToken = handler.ReadToken(authValue[0].Replace("Bearer ", "").Trim());
                            if (jsonToken == null)
                                await _next(httpContext);
                        }
                        catch
                        {
                            await _next(httpContext);
                        }

                        var tokens = jsonToken as JwtSecurityToken;
                        if (tokens?.Claims == null)
                            await _next(httpContext);

                        var userId = tokens.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                        var sessionKey = tokens.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value ?? "Default";

                        if (userId != null)
                        {
                            var user = await userRepo.GetByIdAsync(userId);
                            if (user != null)
                            {
                                //var userSessionKey = user.SessionKey ?? "Default";
                                //if (userSessionKey != sessionKey || user.IsBlocked)
                                //    httpContext.Request.Headers.Remove(Auth);
                            }
                            else
                                httpContext.Request.Headers.Remove(Auth);
                        }

                    }

                //}
            }
            catch (UnauthorizedAccessException ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                return;
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                return;
            }

            await _next(httpContext);
        }
    }
}
