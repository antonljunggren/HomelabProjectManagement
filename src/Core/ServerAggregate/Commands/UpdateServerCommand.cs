using Core.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServerAggregate.Commands
{
    public sealed record UpdateServerCommand : ICommand<Server>
    {
        public Guid ServerId { get; set; }
        public string ServerName { get; set; }
        public string IpAddress { get; set; }
        public string CPU { get; set; }
        public int RAMGigagabytes { get; set; }
        public int DiskSizeGigabytes { get; set; }

        public UpdateServerCommand(Guid serverId, string serverName, string ipAddress, string cPU, int rAMGigagabytes, int diskSizeGigabytes)
        {
            ServerId = serverId;
            ServerName = serverName;
            IpAddress = ipAddress;
            CPU = cPU;
            RAMGigagabytes = rAMGigagabytes;
            DiskSizeGigabytes = diskSizeGigabytes;
        }
    }
}
