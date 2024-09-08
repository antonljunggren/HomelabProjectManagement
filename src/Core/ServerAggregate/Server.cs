using Core.Common;
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

        public Server(string name, ServerIPAddress iPAddress, ServerSpecifications serverSpecifications)
        {
            Name = name;
            IPAddress = iPAddress;
            ServerSpecifications = serverSpecifications;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private Server() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

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
                throw new ArgumentNullException("New Server can not be empty!");
            }

            Name = newName;
        }
    }
}
