using MediatR;
using Shared;

namespace Application.Queries.Statistics
{
    public class GetStatistics : IRequest<IReadOnlyDictionary<string, StatisticsModel>>
    {
    }
}
