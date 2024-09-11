using Core.Common.Commands;
using Core.ServerAggregate.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServerAggregate.Commands.Handlers
{
    public sealed class UpdateServerSecretCommandHandler : ICommandHandler<UpdateServerSecretCommand, ServerSecret>
    {
        private readonly IServerRepository _serverRepository;

        public UpdateServerSecretCommandHandler(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task<ServerSecret> Handle(UpdateServerSecretCommand command, CancellationToken cancellationToken)
        {
            var server = await _serverRepository.GetById(command.ServerId);
            if (server is null)
            {
                throw new ArgumentException($"Server with Id [{command.ServerId}] does not exist");
            }
            
            var secret = server.Secrets.SingleOrDefault(x => x.Id == command.SecretId);
            if (secret is null)
            {
                throw new ArgumentException($"Secret with Id [{command.SecretId}] does not exist");
            }

            secret.UpdateSecretValue(command.SecretValue);
            await _serverRepository.UpdateServer(server);

            return secret;
        }
    }
}
