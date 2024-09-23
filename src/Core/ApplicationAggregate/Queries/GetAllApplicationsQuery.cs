using Core.Common.Query;
using Core.DTOs;

namespace Core.ApplicationAggregate.Queries
{
    public sealed record GetAllApplicationsQuery() : IQuery<List<ApplicationDTO>>
    {
    }
}
