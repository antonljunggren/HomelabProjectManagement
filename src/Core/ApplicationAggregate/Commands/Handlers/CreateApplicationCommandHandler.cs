using Core.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationAggregate.Commands.Handlers
{
    public sealed class CreateApplicationCommandHandler : ICommandHandler<CreateApplicationCommand, Application>
    {
        private readonly IApplicationRepository _repository;

        public CreateApplicationCommandHandler(IApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Application> Handle(CreateApplicationCommand command, CancellationToken cancellationToken)
        {
            var application = new Application(command.ServerId, command.ApplicationName, command.Port, command.CodeRepository);
            return await _repository.AddApplication(application);
        }
    }
}
