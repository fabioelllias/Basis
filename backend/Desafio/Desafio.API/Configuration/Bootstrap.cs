using AutoMapper;
using Desafio.Application;
using Desafio.Core;
using Desafio.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Desafio.API
{
    public static class Bootstrap
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager config)
        {

            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(AutorQueryHandler).Assembly); });

            services.AddScoped<INotificationContext, NotificationContext>();

            services.AddScoped<ICommandResultFactory, CommandResultFactory>();

            services.AddDbContext<LivroContexto>(options => options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork>(provider => provider.GetService<LivroContexto>());

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<ILivroRepository, LivroRepository>();

            var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile()); });

            services.AddSingleton(mapperConfig.CreateMapper());

            return services;
        }
    }
}