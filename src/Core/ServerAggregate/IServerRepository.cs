using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServerAggregate
{
    public interface IServerRepository
    {
        Task<Server?> GetById(Guid id);
        Task DeleteServer(Guid id);
        Task<Server> AddServer(Server server);
        Task<Server> UpdateServer(Server server);
    }
}
