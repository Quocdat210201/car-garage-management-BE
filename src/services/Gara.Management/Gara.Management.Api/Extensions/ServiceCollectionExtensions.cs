using Gara.Management.Application.Data;
using Gara.Management.Application.Repositories;
using Gara.Management.Domain.Entities;
using Gara.Management.Domain.Repositories;
using Gara.Persistance.Abstractions;
using Gara.Persistance.Ef;
using Microsoft.EntityFrameworkCore;

namespace Gara.Management.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GaraManagementDBContent>(options =>
            {
                var connectionString = configuration["Database:ConnectionStrings"];
                options.UseSqlServer(connectionString);
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Car>, EfRepository<GaraManagementDBContent, Car>>();
            services.AddScoped<IRepository<CarBrand>, EfRepository<GaraManagementDBContent, CarBrand>>();
            services.AddScoped<IRepository<CarType>, EfRepository<GaraManagementDBContent, CarType>>();
            services.AddScoped<IRepository<AppointmentSchedule>, EfRepository<GaraManagementDBContent, AppointmentSchedule>>();
            services.AddScoped<IRepository<RepairService>, EfRepository<GaraManagementDBContent, RepairService>>();

            services.AddScoped<ICarRepository, CarRepository>();
            return services;
        }
    }
}
