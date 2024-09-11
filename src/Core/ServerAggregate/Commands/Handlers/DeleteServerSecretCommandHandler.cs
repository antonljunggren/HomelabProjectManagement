using Core.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServerAggregate.Commands.Handlers
{
    internal class DeleteServerSecretCommandHandler : ICommandHandler<DeleteServerSecretCommand, NoResult>
    {
        private readonly IServerRepository _repository;

        public DeleteServerSecretCommandHandler(IServerRepository repository)
        {
            _repository = repository;
        }

        public async Task<NoResult> Handle(DeleteServerSecretCommand command, CancellationToken cancellationToken)
        {
            var server = await _repository.GetById(command.ServerId);
            if (server is null)
            {
                throw new ArgumentException($"Server with Id [{command.ServerId}] does not exist");
            }

            server.RemoveSecret(command.SecretId);
            await _repository.UpdateServer(server);

            return new NoResult();
        }
    }
}
