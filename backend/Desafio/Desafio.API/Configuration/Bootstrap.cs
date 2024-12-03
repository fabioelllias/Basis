using Desafio.Infrastructure.Domain;
using Desafio.Infrastructure.Interface;
using Desafio.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Desafio.Infrastructure.Contexto;

namespace Desafio.API
{
    public static class Bootstrap
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager config)
        {
            services.AddDbContext<LivroContexto>(options => options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork>(provider => provider.GetService<LivroContexto>());

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            return services;
        }
    }
}
