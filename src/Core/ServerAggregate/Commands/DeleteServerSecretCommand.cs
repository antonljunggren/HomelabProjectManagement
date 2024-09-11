using Core.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServerAggregate.Commands
{
    public sealed record DeleteServerSecretCommand(Guid ServerId, Guid SecretId) : ICommand
    {
    }
}
