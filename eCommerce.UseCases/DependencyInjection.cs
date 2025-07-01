using eCommerce.UseCases.Users.Login;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.UseCases
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(LoginUserCommandValidator).Assembly;
            services.AddMediatR(cfg =>
                            cfg.RegisterServicesFromAssembly(assembly));
         
            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
