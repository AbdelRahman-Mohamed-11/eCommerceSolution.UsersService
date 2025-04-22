using eCommerce.Core.Interfaces;
using eCommerce.Infrastructure.Repositories;
using eCommerce.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
