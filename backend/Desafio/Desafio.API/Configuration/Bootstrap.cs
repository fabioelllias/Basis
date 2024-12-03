using Desafio.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Desafio.API
{
    public static class Bootstrap
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager config)
        {

            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(ICommandResult).Assembly); });

            services.AddScoped<INotificationContext, NotificationContext>();

            services.AddDbContext<LivroContexto>(options => options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork>(provider => provider.GetService<LivroContexto>());

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            return services;
        }
    }
}
