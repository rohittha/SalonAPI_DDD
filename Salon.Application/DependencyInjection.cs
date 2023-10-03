using Microsoft.Extensions.DependencyInjection;
using Salon.Application.Services.Authentication;
using Salon.Application.Services.Authentication.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthCommandService, AuthCommandService>();
            services.AddScoped<IAuthQueryService, AuthQueryService>();
            return services;
        }
    }
}
