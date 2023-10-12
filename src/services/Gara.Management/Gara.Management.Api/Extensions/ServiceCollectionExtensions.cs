using Gara.Management.Application.Data;
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
    }
}
