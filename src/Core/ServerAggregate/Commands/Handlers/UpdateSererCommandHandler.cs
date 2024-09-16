using Core.Common.Commands;
using Core.Common.Query;
using Core.DTOs;
using Core.ServerAggregate.Queries;
using Core.ServerAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServerAggregate.Commands.Handlers
{
    public sealed class UpdateSererCommandHandler : ICommandHandler<UpdateServerCommand, Server>
    {
        private readonly IServerRepository _repository;
        private readonly IQueryDispatcher _queryDispatcher;

        public UpdateSererCommandHandler(IServerRepository repository, IQueryDispatcher queryDispatcher)
        {
            _repository = repository;
            _queryDispatcher = queryDispatcher;
        }

        public async Task<Server> Handle(UpdateServerCommand command, CancellationToken cancellationToken)
        {
            var server = await _repository.GetById(command.ServerId);
            if(server is null)
            {
                throw new ArgumentException($"Server with Id [{command.ServerId}] does not exist");
            }

            var newIpAddress = new ServerIPAddress(command.IpAddress);
            var newServerSpecifications = new ServerSpecifications(command.CPU, command.RAMGigagabytes, command.DiskSizeGigabytes);

            var allServers = await _queryDispatcher.Dispatch<GetAllServersQuery, List<ServerDto>>(new(), cancellationToken);

            if (allServers.Any(s => s.Name == command.ServerName))
            {
                throw new InvalidOperationException($"Server with name [{command.ServerName}] already exists!");
            }

            if (allServers.Any(s => s.IPAddress == command.IpAddress))
            {
                throw new InvalidOperationException($"Server with IP address [{command.IpAddress}] already exists!");
            }

            server.UpdateName(command.ServerName);
            server.UpdateIpAddress(newIpAddress);
            server.UpdateServerSpecifications(newServerSpecifications);

            return await _repository.UpdateServer(server);
        }
    }
}
