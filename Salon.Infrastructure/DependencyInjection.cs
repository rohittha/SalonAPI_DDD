using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Salon.Application.Common.Interfaces.Authentication;
using Salon.Application.Common.Interfaces.Services;
using Salon.Application.Services.Persistence;
using Salon.Infrastructure.Authentication;
using Salon.Infrastructure.Persistence;
using Salon.Infrastructure.Services;

namespace Salon.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
