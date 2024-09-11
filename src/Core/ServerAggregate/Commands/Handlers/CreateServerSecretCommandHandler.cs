using Core.Common.Commands;
using Core.ServerAggregate.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServerAggregate.Commands.Handlers
{
    public sealed class CreateServerSecretCommandHandler : ICommandHandler<CreateServerSecretCommand, ServerSecret>
    {
        private readonly IServerRepository _serverRepository;

        public CreateServerSecretCommandHandler(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task<ServerSecret> Handle(CreateServerSecretCommand command, CancellationToken cancellationToken)
        {
            var secret = new ServerSecret(command.SecretName, command.SecretValue);
            var server = await _serverRepository.GetById(command.ServerId);
            if (server is null)
            {
                throw new ArgumentException($"Server with Id [{command.ServerId}] does not exist");
            }

            server.AddSecret(secret);
            await _serverRepository.UpdateServer(server);

            return secret;
        }
    }
}
