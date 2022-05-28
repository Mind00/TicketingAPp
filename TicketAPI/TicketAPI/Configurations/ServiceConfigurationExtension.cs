using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketAPI.Services.Implementation;
using TicketAPI.Services.Interface;

namespace TicketAPI.Configurations
{
    public static class ServiceConfigurationExtension
    {
        internal static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            
            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<ITicketService, TicketService>();
            return services;
        }
    }
}
