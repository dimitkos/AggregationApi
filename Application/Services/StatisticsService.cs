using Domain.Aggregates;
using Gateway.Services;
using Shared;
using System.Collections.Concurrent;

namespace Application.Services
{
    class StatisticsService : IStatisticsService
    {
        private readonly ConcurrentDictionary<string, Statistics> _stats = new();

        public void Record(string apiName, double elapsedMs)
        {
            //_stats.AddOrUpdate(apiName,_ => Statistics.Initialize().Update(elapsedMs),(_, existing) => existing.Update(elapsedMs));

            var stat = _stats.GetOrAdd(apiName, _ => Statistics.Initialize());

            lock (stat)
            {
                stat.Update(elapsedMs);
            }
        }

        public IReadOnlyDictionary<string, StatisticsModel> GetStats() =>
            _stats.ToDictionary(
                kvp => kvp.Key,
                kvp => new StatisticsModel(
                    kvp.Value.TotalRequests,
                    kvp.Value.TotalResponseTimeMs,
                    kvp.Value.FastCount,
                    kvp.Value.AverageCount,
                    kvp.Value.SlowCount,
                    kvp.Value.AverageResponseTime
                )
            );

    }
}
