namespace Domain
{
    public abstract class AggregateRoot<TKey>
    {
        protected TKey Key { get; }

        protected AggregateRoot(TKey key)
        {
            Key = key;
        }
    }
}
