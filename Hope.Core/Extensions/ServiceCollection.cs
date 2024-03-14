using FluentValidation;
using Hope.Core.ExternalService;
using Hope.Core.Interfaces;
using Hope.Core.Service;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Hope.Core.Extensions
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddCore(this IServiceCollection services,IConfigurationManager  configuration)
        {
            

           services.AddMapping().AddCollection().AddValidators().AddMediator(); 

            



            return services;
        }

        private static IServiceCollection AddMapping(this IServiceCollection services) {

            services.AddMapster();
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

             return services;

        }

        private static IServiceCollection AddCollection(this IServiceCollection services)
        {

            services.AddScoped<IAuthService, AuthService>();


            services.AddScoped<IMediaService, MediaService>();

            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IAiPostServices, AiPostServices>();

            services.AddTransient<IMailService, MailService>();


            return services;

        }
        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            return services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
        private static IServiceCollection AddMediator(this IServiceCollection services)
        {
            return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
