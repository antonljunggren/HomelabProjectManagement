using Core.ApplicationAggregate;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal sealed class ApplicationRepository : IApplicationRepository
    {
        private WriteDbContext _context;

        public ApplicationRepository(WriteDbContext context)
        {
            _context = context;
        }

        public async Task<Application> AddApplication(Application application)
        {
            _context.Applications.Add(application);
            var rows = await _context.SaveChangesAsync();

            if (rows <= 0)
            {
                throw new Exception("Application not created!");
            }

            return application;
        }

        public async Task DeleteApplication(Guid id)
        {
            var application = await GetById(id);

            if (application is not null)
            {
                _context.Applications.Remove(application);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<Application>> GetAllAsReadOnly()
        {
            var applications = await _context.Applications.AsNoTracking().ToListAsync();
            return applications.AsReadOnly();
        }

        public async Task<Application?> GetById(Guid id)
        {
            var application = await _context.Applications.SingleOrDefaultAsync(a =>  a.Id == id);
            return application;
        }

        public async Task<List<Application>> GetByServerId(Guid serverId)
        {
            var applications = await _context.Applications.Where(a =>  a.ServerId.Equals(serverId)).ToListAsync();
            return applications;
        }

        public async Task<Application> UpdateApplication(Application application)
        {
            _context.Applications.Update(application);
            await _context.SaveChangesAsync();

            return application;
        }
    }
}
