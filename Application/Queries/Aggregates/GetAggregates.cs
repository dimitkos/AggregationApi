using MediatR;
using Shared;

namespace Application.Queries.Aggregates
{
    public class GetAggregates : IRequest<AggregationModel>
    {
    }
}
