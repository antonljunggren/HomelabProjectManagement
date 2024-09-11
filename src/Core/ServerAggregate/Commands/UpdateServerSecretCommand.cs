using Core.Common.Commands;
using Core.ServerAggregate.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServerAggregate.Commands
{
    public sealed record UpdateServerSecretCommand(Guid ServerId, Guid SecretId, string SecretValue) : ICommand<ServerSecret>
    {
    }
}
