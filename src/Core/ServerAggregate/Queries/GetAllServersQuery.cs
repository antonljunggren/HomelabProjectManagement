using Core.Common.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServerAggregate.Queries
{
    public sealed record GetAllServersQuery : IQuery<List<Server>>
    {
    }
}
