using Domain.Aggregates;

namespace Application.Services.Infrastructure
{
    public interface IAggregatesPersistence
    {
        Task StoreAllAggregates(AggregationEntity entity);
    }
}
