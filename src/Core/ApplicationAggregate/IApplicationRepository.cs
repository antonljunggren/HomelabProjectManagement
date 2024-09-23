using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationAggregate
{
    public interface IApplicationRepository
    {
        Task<Application?> GetById(Guid id);
        Task<List<Application>> GetByServerId(Guid serverId);
        Task<IReadOnlyList<Application>> GetAllAsReadOnly();
        Task DeleteApplication(Guid id);
        Task<Application> AddApplication(Application application);
        Task<Application> UpdateApplication(Application application);
    }
}
