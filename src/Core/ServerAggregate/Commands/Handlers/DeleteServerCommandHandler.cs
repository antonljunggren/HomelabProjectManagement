using Core.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServerAggregate.Commands.Handlers
{
    public sealed class DeleteServerCommandHandler : ICommandHandler<DeleteServerCommand, NoResult>
    {
        private readonly IServerRepository _repository;

        public DeleteServerCommandHandler(IServerRepository repository)
        {
            _repository = repository;
        }

        public async Task<NoResult> Handle(DeleteServerCommand command, CancellationToken cancellationToken)
        {
            await _repository.DeleteServer(command.ServerId);

            return new NoResult();
        }
    }
}
