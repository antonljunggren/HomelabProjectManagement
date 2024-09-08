using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServerAggregate.ValueObjects
{
    public sealed class ServerSpecifications : ValueObject
    {
        public string CPU { get; private set; }
        public int RAMGigagabytes { get; private set; }
        public int DiskSizeGigabytes { get; private set; }

        public ServerSpecifications(string cpu, int ramGigagabytes, int diskSizeGigabytes)
        {
            if (String.IsNullOrWhiteSpace(cpu))
            {
                throw new ArgumentException("CPU name is empty!");
            }

            if (ramGigagabytes <= 0)
            {
                throw new ArgumentException($"RAM size [{ramGigagabytes}] is invalid!");
            }

            if (diskSizeGigabytes <= 0)
            {
                throw new ArgumentException($"Disk size [{diskSizeGigabytes}] is invalid!");
            }

            CPU = cpu;
            RAMGigagabytes = ramGigagabytes;
            DiskSizeGigabytes = diskSizeGigabytes;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private ServerSpecifications() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CPU;
            yield return RAMGigagabytes;
            yield return DiskSizeGigabytes;
        }
    }
}
