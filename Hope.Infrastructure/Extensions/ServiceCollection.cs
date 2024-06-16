using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Hope.Infrastructure.InternalServices.jwtTokenGenerator;
using Hope.Infrastructure.Jobs;
using Hope.Infrastructure.Repos;
using Hope.Infrastructure.Services.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Quartz;
using System.Globalization;

namespace Hope.Infrastructure.Extensions
{
    public static class ServiceCollection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationManager configuration)
        {
            services.AddIdentity<User, IdentityRole>(o=> { 
                
                 o.User.RequireUniqueEmail = true;
                 o.Password.RequiredLength = 8;
            
            }).AddEntityFrameworkStores<AppDBContext>();

            

            services.AddDbContext<AppDBContext>(option => option.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("defstr")));

            services.AddScoped<IUnitofWork, UnitofWork>();

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
           
            services.AddSPecialLocalization();



            return services;
        }
        public static IServiceCollection AddSPecialLocalization(this IServiceCollection services)
        {

            services.AddLocalization();
            services.
                AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

            services.AddMvc().AddDataAnnotationsLocalization(op =>
            {
                op.DataAnnotationLocalizerProvider = (type, factory) =>
                factory.Create(typeof(JsonStringLocalizerFactory));
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {

                var supportedCultures = new[] {
                  new CultureInfo("en-US"),
                  new CultureInfo("ar-EG")

                 };
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(culture: supportedCultures[0]);
                options.SupportedCultures = supportedCultures;
            });

            return services;
        }
        public static IServiceCollection AddSpecialQuartz(this IServiceCollection services)
        {
            services.AddQuartz(o =>
            {
                o.UseMicrosoftDependencyInjectionJobFactory();

                var jobkey = JobKey.Create(nameof(DeletingPostsJob));

                o.AddJob<DeletingPostsJob>(jobkey)
                .AddTrigger(t => t.ForJob(jobkey)
                .WithSimpleSchedule(s => s.WithIntervalInHours(10).RepeatForever()));

            });

            services.AddQuartzHostedService(o =>
            {

                o.WaitForJobsToComplete = true;

            });

            return services;

        }
    }
}
