using Gateway.Api;
using Gateway.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Gateway.Decorators
{
    class StatisticsApiDecorator<TResponse> : IApiClient<TResponse>
    {
        private readonly IApiClient<TResponse> _client;
        private readonly IStatisticsService _statistics;

        public StatisticsApiDecorator(IApiClient<TResponse> client, IStatisticsService statistics)
        {
            _client = client;
            _statistics = statistics;
        }

        public async Task<TResponse> Get(string relativePath, string clientName, IDictionary<string, string>? queryStringParams = null)
        {
            var sw = Stopwatch.StartNew();

            var result = await _client.Get(relativePath, clientName, queryStringParams);

            sw.Stop();
            _statistics.Record(clientName, sw.ElapsedMilliseconds);

            return result;
        }
    }
}
