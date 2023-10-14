using Gara.Management.Application.Data;
using Gara.Management.Domain.Entities;
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
            return services;
        }
    }
}
