using Gateway.Services;
using MediatR;
using Shared;

namespace Application.Queries.Statistics
{
    class GetStatisticsHandler : IRequestHandler<GetStatistics, IReadOnlyDictionary<string, StatisticsModel>>
    {
        private readonly IStatisticsService _statisticsService;

        public GetStatisticsHandler(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public Task<IReadOnlyDictionary<string, StatisticsModel>> Handle(GetStatistics request, CancellationToken cancellationToken) =>
            Task.FromResult(_statisticsService.GetStats());
    }
}
