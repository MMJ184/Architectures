using Assignment.Repository.Abstract;
using Assignment.Repository.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment.Repository.Common
{
    public static class RepositoryRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IUsersToRolesRepository, UsersToRolesRepository>();

            return services;
        }
    }
}
