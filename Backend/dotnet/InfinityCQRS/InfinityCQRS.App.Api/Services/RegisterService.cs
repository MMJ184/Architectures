using InfinityCQRS.App.Handlers.Users;
using InfinityCQRS.App.Repository.Common;

namespace InfinityCQRS.App.Api.Services
{
    public static class RegisterService
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddUserHandler).Assembly));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
    }
}
