using InfinityDapper.Repository.Abstract;
using InfinityDapper.Repository.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace InfinityDapper.Repository.Common
{
    public static class RepositoryRegistration
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
