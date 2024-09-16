using Core.ServerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public sealed class ServerDto
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string IPAddress { get; private set; }
        public ServerSpecificationsDto ServerSpecifications { get; private set; }
        public List<ServerSecretDto> Secrets { get; private set; }

        public ServerDto(Guid id, string name, string iPAddress, ServerSpecificationsDto specifications, List<ServerSecretDto> secrets)
        {
            Id = id;
            Name = name;
            IPAddress = iPAddress;
            ServerSpecifications = specifications;
            Secrets = secrets;
        }

        public static ServerDto FromDomain(Server server)
        {
            var serverSpecs = ServerSpecificationsDto.FromDomain(server.ServerSpecifications);
            var serverSecrets = server.Secrets.Select(s => ServerSecretDto.FromDomain(s)).ToList();

            return new ServerDto(server.Id, server.Name, server.IPAddress.Value, serverSpecs, serverSecrets);
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private ServerDto() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    }
}
