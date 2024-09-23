using Core.ApplicationAggregate.Queries;
using Core.Common.Query;
using Core.DTOs;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.QueryHandlers.Application
{
    internal sealed class GetAllApplicationsQueryHandler : IQueryHandler<GetAllApplicationsQuery, List<ApplicationDTO>>
    {
        private readonly ReadDbContext _dbContext;

        public GetAllApplicationsQueryHandler(ReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ApplicationDTO>> Handle(GetAllApplicationsQuery query, CancellationToken cancellationToken)
        {
            var applications = await _dbContext.Applications.AsNoTracking().ToListAsync();
            return applications;
        }
    }
}
