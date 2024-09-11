using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServerAggregate.Entites
{
    public class ServerSecret : Entity
    {
        public string SecretName { get; private set; }
        public string SecretValue { get; private set; }

        public ServerSecret(string secretName, string secretValue)
        {
            SecretName = secretName;
            SecretValue = secretValue;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private ServerSecret() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    
        public void UpdateSecretValue(string secretValue)
        {
            if (string.IsNullOrWhiteSpace(secretValue))
            {
                throw new ArgumentNullException("New Server secret can not be empty!");
            }

            SecretValue = secretValue;
        }
    }
}
