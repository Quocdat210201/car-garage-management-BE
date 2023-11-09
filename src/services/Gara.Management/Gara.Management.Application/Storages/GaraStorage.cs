using Gara.Cache.Redis;
using Gara.Management.Domain.Storages;

namespace Gara.Management.Application.Storages
{
    public class GaraStorage : RedisCacheStorage<string, object>, IGaraStorage
    {
    }
}
