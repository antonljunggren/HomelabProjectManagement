using Core.ServerAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public sealed class ServerSpecificationsDto
    {
        public string CPU { get; private set; }
        public int RAMGigagabytes { get; private set; }
        public int DiskSizeGigabytes { get; private set; }

        public ServerSpecificationsDto(string cPU, int rAMGigagabytes, int diskSizeGigabytes)
        {
            CPU = cPU;
            RAMGigagabytes = rAMGigagabytes;
            DiskSizeGigabytes = diskSizeGigabytes;
        }

        public static ServerSpecificationsDto FromDomain(ServerSpecifications specs)
        {
            return new ServerSpecificationsDto(specs.CPU, specs.RAMGigagabytes, specs.DiskSizeGigabytes);
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private ServerSpecificationsDto() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    }
}
