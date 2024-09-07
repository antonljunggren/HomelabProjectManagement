using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServerAggregate.ValueObjects
{
    public sealed class ServerIPAddress : ValueObject
    {
        public string Value { get; private set; }

        public ServerIPAddress(string ipAddress)
        {
            if(IPAddress.TryParse(ipAddress, out _))
            {
                Value = ipAddress;
            }
            else
            {
                throw new ArgumentException($"IP address: {ipAddress} is not valid!");
            }
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private ServerIPAddress() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
