using Common;
using Domain.Aggregates;
using Gateway.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using Shared;

namespace Application.Jobs
{
    public class StatitisticsPerformanceJob : IJob
    {
        private readonly ILogger<StatitisticsPerformanceJob> _logger;
        private readonly IStatisticsService _statisticsService;
        private readonly StatisticsPerformanceConfigurationJob _configuration;

        public StatitisticsPerformanceJob(
            ILogger<StatitisticsPerformanceJob> logger,
            IStatisticsService statisticsService,
            IOptions<StatisticsPerformanceConfigurationJob> options)
        {
            _logger = logger;
            _statisticsService = statisticsService;
            _configuration = options.Value;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var stats = _statisticsService.GetStats();

            foreach (var item in stats)
            {
                var statistic = item.Value;

                if (statistic.TotalRequests == 0)
                    continue;

                var latestAverage = MapTo(statistic).GetLastMinutesAverage(_configuration.MinutesToCalculate);

                if (latestAverage > statistic.AverageResponseTime * _configuration.ThresholdMultiplier)
                    _logger.LogWarning($"Performance anomaly detected {item.Key}. AverageResponseTime:{statistic.AverageResponseTime} last AverageResponseTime {latestAverage} ms");
            }

            return Task.CompletedTask;
        }

        //add in a utility interface
        private Statistics MapTo(StatisticsModel statisticsModel)
        {
            var stat = new Statistics(
                apiName: statisticsModel.ApiName,
                totalRequests: statisticsModel.TotalRequests,
                totalResponseTimeMs: statisticsModel.TotalResponseTimeMs,
                fastCount: statisticsModel.FastCount,
                averageCount: statisticsModel.AverageCount,
                slowCount: statisticsModel.SlowCount,
                averageResponseTime: statisticsModel.AverageResponseTime);

            foreach (var req in statisticsModel.RecentRequests)
                stat.AddRecentRequest(req.Timestamp, req.ElapsedMs);

            return stat;
        }
    }
}
