using Microsoft.EntityFrameworkCore;
using time_of_your_life.Application.Managers;
using time_of_your_life.Application.Profiles;
using time_of_your_life.Domain.Repositories.Clock;
using time_of_your_life.Filters;
using time_of_your_life.Infrastructure.Data.Context;

namespace time_of_your_life.Configuration
{
    public static class ConfigurationServices
    {
        public static IServiceCollection RegisterDbContext(this IServiceCollection services, ConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlite(connectionString));

            return services;
        }

        public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
        {
            services.AddScoped<ApiExceptionFilter>();

            return services;
        }

        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(c =>
            {
                c.AllowNullCollections = true;
            }, typeof(MappingProfile));

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.RegisterManagers();
            services.RegisterRepositories();

            return services;
        }

        private static IServiceCollection RegisterManagers(this IServiceCollection services)
        {
            services.AddScoped<IClockManager, ClockManager>();

            return services;
        }

        private static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClockRepository, ClockRepository>();

            return services;
        }
    }
}
