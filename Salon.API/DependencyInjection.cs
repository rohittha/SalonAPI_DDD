using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Salon.API.Common.Mapping;
using Salon.API.Errors;
using System.Reflection;

namespace Salon.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services) 
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, SalonProblemDetailsFactory>();
            services.AddMappings();
            return services;
        }
    }
}
