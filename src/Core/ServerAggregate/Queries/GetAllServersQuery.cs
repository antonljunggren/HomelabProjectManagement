using Core.Common.Query;
using Core.DTOs;

namespace Core.ServerAggregate.Queries
{
    public sealed record GetAllServersQuery : IQuery<List<ServerDto>>
    {
    }
}
