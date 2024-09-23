using Core.ApplicationAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public sealed class ApplicationDTO
    {
        public Guid Id { get; private set; }
        public Guid? ServerId { get; private set; }
        public string ApplicationName { get; private set; }
        public ushort Port { get; private set; }
        public string CodeRepository { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private ApplicationDTO() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    
        public static ApplicationDTO FromDomain(Application application)
        {
            return new ApplicationDTO
            {
                Id = application.Id,
                ServerId = application.ServerId,
                ApplicationName = application.ApplicationName,
                Port = application.Port,
                CodeRepository = application.CodeRepository,
            };
        }
    }
}
