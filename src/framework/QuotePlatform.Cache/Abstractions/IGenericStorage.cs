namespace QuotePlatform.Cache.Abstractions
{
    public interface IGenericStorage<TKey, TValue>
    {
        TValue Get(TKey key);

        void Set(TKey key, TValue value);

        void Remove(TKey key);

        void Clear();
    }
}
