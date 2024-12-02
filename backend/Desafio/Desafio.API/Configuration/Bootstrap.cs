using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Desafio.API
{
    public static class Bootstrap
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager config)
        {
            return services;
        }
    }
}
