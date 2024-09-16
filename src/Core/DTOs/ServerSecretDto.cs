using Core.ServerAggregate.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public sealed class ServerSecretDto
    {
        public Guid Id { get; private set; }
        public string SecretName { get; private set; }
        public string SecretValue { get; private set; }

        public ServerSecretDto(Guid id, string secretName, string secretValue)
        {
            Id = id;
            SecretName = secretName;
            SecretValue = secretValue;
        }

        public static ServerSecretDto FromDomain(ServerSecret secret)
        {
            return new ServerSecretDto(secret.Id, secret.SecretName, secret.SecretValue);
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private ServerSecretDto() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    }
}
