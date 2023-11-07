using Assignment.Repository.Common;
using Assignment.Service.Abstract;
using Assignment.Service.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment.Service.Common
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccontService>();


            services.RegisterServices();
            return services;
        }
    }
}
