using Core.Common.Query;
using Core.ServerAggregate;
using Core.ServerAggregate.Queries;
using Core.ServerAggregate.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.QueryHandlers.Server
{
    internal sealed class GetAllServersQueryHandler : IQueryHandler<GetAllServersQuery, List<Core.ServerAggregate.Server>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAllServersQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Core.ServerAggregate.Server>> Handle(GetAllServersQuery query, CancellationToken cancellationToken)
        {
            var servers = await _dbContext.Servers.AsNoTracking().ToListAsync();
            return servers;
        }
    }
}
