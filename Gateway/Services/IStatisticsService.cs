using Shared;
using System.Collections.Generic;

namespace Gateway.Services
{
    public interface IStatisticsService
    {
        void Record(string apiName, double elapsedMs);
        IReadOnlyDictionary<string, StatisticsModel> GetStats();
    }
}
