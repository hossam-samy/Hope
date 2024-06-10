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
            

           services.AddMapping().AddCollection().Addasd().AddMediator().AddSignalR(); 

            



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



            services.AddScoped<IMediaService, MediaService>();
           
            services.AddScoped<IAiPostServices, AiPostServices>();

            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IRecommendationService, RecommendationService>();
            services.AddTransient<IFaceRecognitionService, FaceRecognitionService>();


            return services;

        }
        private static IServiceCollection Addasd(this IServiceCollection services)
        {
            return services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
        private static IServiceCollection AddMediator(this IServiceCollection services)
        {
            return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
