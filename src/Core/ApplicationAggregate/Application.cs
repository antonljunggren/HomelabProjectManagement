using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationAggregate
{
    public class Application : Entity, IAggregateRoot
    {
        public Guid? ServerId { get; private set; }
        public string ApplicationName { get; private set; }
        public ushort Port { get; private set; }
        public string CodeRepository { get; private set; }

        public Application(Guid? serverId, string name, int port, string codeRepository)
        {
            UpdateServerId(serverId);
            UpdateName(name);
            UpdatePort(port);
            UpdateCodeRepository(codeRepository);

            if(ApplicationName is null || CodeRepository is null)
            {
                throw new Exception($"Name:{ApplicationName}, or Repo:{CodeRepository} cannot be null");
            }
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private Application() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public void UpdateServerId(Guid? serverId)
        {
            ServerId = serverId;
        }

        public void UpdatePort(int port)
        {
            if(port < 1 || port > ushort.MaxValue)
            {
                throw new ArgumentOutOfRangeException($"A port cannot be {port}, must be < {ushort.MaxValue} and > 0");
            }

            port = (ushort)port;
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentNullException("New Server name can not be empty!");
            }

            ApplicationName = newName;
        }

        public void UpdateCodeRepository(string newRepository)
        {
            if (string.IsNullOrWhiteSpace(newRepository))
            {
                throw new ArgumentNullException("New Code Repository name can not be empty!");
            }

            CodeRepository = newRepository;
        }
    }
}
