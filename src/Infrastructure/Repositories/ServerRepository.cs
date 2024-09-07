using Core.ServerAggregate;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal sealed class ServerRepository : IServerRepository
    {
        private ApplicationDbContext _context;

        public ServerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task DeleteServer(Guid id)
        {
            var server = await GetById(id);

            if(server is not null)
            {
                _context.Servers.Remove(server);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Server?> GetById(Guid id)
        {
            var server = await _context.Servers.SingleOrDefaultAsync(s => s.Id == id);
            return server;
        }
    }
}
