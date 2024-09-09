using Core.Common.Commands;
using Core.Common.Query;
using Core.ServerAggregate;
using Core.ServerAggregate.Commands;
using Core.ServerAggregate.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        private readonly ILogger<ServerController> _logger;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public ServerController(ILogger<ServerController> logger, IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public async Task<IEnumerable<Server>> GetAll(CancellationToken cancellationToken)
        {
            var cmd = new GetAllServersQuery();

            return await _queryDispatcher.Dispatch<GetAllServersQuery, List<Server>>(cmd, cancellationToken);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateServer([FromBody] UpdateServerCommand serverCmd, CancellationToken cancellationToken)
        {
            var server = await _commandDispatcher.Dispatch<UpdateServerCommand, Server>(serverCmd, cancellationToken);

            return Ok(server);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateServer([FromBody] CreateServerCommand serverCmd, CancellationToken cancellationToken)
        {
            var server = await _commandDispatcher.Dispatch<CreateServerCommand, Server>(serverCmd, cancellationToken);

            return Ok(server);
        }

        [HttpPost("delete/{serverId}")]
        public async Task<IActionResult> DeleteServer(Guid serverId, CancellationToken cancellationToken)
        {
            var cmd = new DeleteServerCommand(serverId);
            await _commandDispatcher.Dispatch<DeleteServerCommand, NoResult>(cmd, cancellationToken);

            return Ok();
        }
    }
}
