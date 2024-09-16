using Core.Common;
using Core.ServerAggregate.Entites;
using Core.ServerAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServerAggregate
{
    public sealed class Server : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public ServerIPAddress IPAddress { get; private set; }
        public ServerSpecifications ServerSpecifications { get; private set; }
        private ICollection<ServerSecret> _secrets;
        public IReadOnlyList<ServerSecret> Secrets => _secrets.ToList().AsReadOnly();

        public Server(string name, ServerIPAddress iPAddress, ServerSpecifications serverSpecifications, ICollection<ServerSecret> secrets)
        {
            Name = name;
            IPAddress = iPAddress;
            ServerSpecifications = serverSpecifications;
            _secrets = secrets;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private Server() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public void AddSecret(ServerSecret secret)
        {
            _secrets.Add(secret);
        }

        public void RemoveSecret(Guid secretId)
        {
            var secret = _secrets.SingleOrDefault(s => s.Id == secretId);
            if(secret is null)
            {
                throw new InvalidOperationException("Secret does not exist");
            }

            _secrets.Remove(secret);
        }

        public void UpdateIpAddress(ServerIPAddress newIpAddress)
        {
            IPAddress = newIpAddress;
        }

        public void UpdateServerSpecifications(ServerSpecifications serverSpecifications)
        {
            ServerSpecifications = serverSpecifications;
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentNullException("New Server name can not be empty!");
            }

            Name = newName;
        }
    }
}
