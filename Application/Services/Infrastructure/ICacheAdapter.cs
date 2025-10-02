namespace Application.Services.Infrastructure
{
    public interface ICacheAdapter<TKey, TEntity>
        where TKey : notnull
    {
        void Set(TKey key, TEntity value);
        void Remove(TKey[] keys);

        TEntity? TryGet(TKey key);
        Dictionary<TKey, TEntity> GetMany(TKey[] keys);
    }
}
