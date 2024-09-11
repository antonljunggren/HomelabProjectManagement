using Core.Common.Commands;
using Core.Common.Query;
using Core.ServerAggregate.Entites;
using Core.ServerAggregate.Queries;
using Core.ServerAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServerAggregate.Commands.Handlers
{
    public sealed class CreateServerCommandHandler : ICommandHandler<CreateServerCommand, Server>
    {
        private readonly IServerRepository _repository;
        private readonly IQueryDispatcher _queryDispatcher;

        public CreateServerCommandHandler(IServerRepository repository, IQueryDispatcher queryDispatcher)
        {
            _repository = repository;
            _queryDispatcher = queryDispatcher;
        }

        public async Task<Server> Handle(CreateServerCommand command, CancellationToken cancellationToken)
        {
            var ipAddress = new ServerIPAddress(command.IpAddress);
            var serverSpecifications = new ServerSpecifications(command.CPU, command.RAMGigagabytes, command.DiskSizeGigabytes);
            var server = new Server(command.ServerName, ipAddress, serverSpecifications, new List<ServerSecret>());

            var query = new GetAllServersQuery();
            var allServers = await _queryDispatcher.Dispatch<GetAllServersQuery, List<Server>>(query, cancellationToken);

            if(allServers.Any(s => s.Name == command.ServerName))
            {
                throw new InvalidOperationException($"Server with name [{command.ServerName}] already exists!");
            }

            if (allServers.Any(s => s.IPAddress.Value == command.IpAddress))
            {
                throw new InvalidOperationException($"Server with IP address [{command.IpAddress}] already exists!");
            }

            return await _repository.AddServer(server);
        }
    }
}
