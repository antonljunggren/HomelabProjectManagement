using Core.Common.Query;
using Core.DTOs;
using Core.ServerAggregate.Queries;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.QueryHandlers.Server
{
    internal sealed class GetAllServersQueryHandler : IQueryHandler<GetAllServersQuery, List<ServerDto>>
    {
        private readonly ReadDbContext _dbContext;

        public GetAllServersQueryHandler(ReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ServerDto>> Handle(GetAllServersQuery query, CancellationToken cancellationToken)
        {
            var servers = await _dbContext.Servers.AsNoTracking().ToListAsync();
            return servers;
        }
    }
}
