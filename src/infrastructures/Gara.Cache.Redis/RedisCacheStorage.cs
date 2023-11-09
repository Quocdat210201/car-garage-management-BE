using Gara.Cache.Abstractions;
using Newtonsoft.Json;
using System.Text;

namespace Gara.Cache.Redis
{
    public class RedisCacheStorage<TKey, TValue> : IGenericStorage<TKey, TValue>
    {
        private TimeSpan? _expiry { get; set; }

        public RedisCacheStorage(TimeSpan? expiry = null)
        {
            _expiry = expiry;
        }

        public void Clear()
        {
            foreach (var item in RedisDatabase.GetEndPoints())
            {
                var s = RedisDatabase.GetServer(item);
                s.FlushDatabase();
            }
        }

        public TValue Get(TKey key)
        {
            var keyBuilder = $"Gara_{key}";
            if (string.IsNullOrEmpty(keyBuilder))
            {
                return default;
            }

            var result = RedisDatabase.GetDatabase().StringGet(keyBuilder);
            return result == default ? default : JsonConvert.DeserializeObject<TValue>(Encoding.UTF8.GetString(result));
        }

        public void Remove(TKey key)
        {
            RedisDatabase.GetDatabase().KeyDelete($"Gara_{key}");
        }

        public void Set(TKey key, TValue value)
        {
            var keyBuilder = $"Gara_{key}";
            if (string.IsNullOrEmpty(keyBuilder))
            {
                return;
            }

            RedisDatabase.GetDatabase().StringSet(keyBuilder, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), _expiry);
        }
    }
}
