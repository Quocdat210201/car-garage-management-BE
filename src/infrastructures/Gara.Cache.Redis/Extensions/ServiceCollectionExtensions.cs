using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gara.Cache.Redis.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            RedisDatabase.InitializeConnectionString(configuration["Redis:Host"], Convert.ToInt32(configuration["Redis:Port"]), configuration["Redis:Password"]);
            return services;
        }
    }
}