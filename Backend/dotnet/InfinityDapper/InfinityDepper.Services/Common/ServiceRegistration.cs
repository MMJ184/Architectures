using InfinityDapper.Dapper;
using InfinityDapper.Repository.Common;
using InfinityDapper.Services.Abstract;
using InfinityDapper.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace InfinityDapper.Services.Common
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IDapperContext, DapperContext>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();

            services.RegisterRepositories();
            return services;
        }
    }
}
