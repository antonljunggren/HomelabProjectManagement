using Core.Common.Commands;
using Core.Common.Query;
using Core.DTOs;
using Core.ServerAggregate;
using Core.ServerAggregate.Commands;
using Core.ServerAggregate.Entites;
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
        public async Task<IEnumerable<ServerDto>> GetAll(CancellationToken cancellationToken)
        {
            var cmd = new GetAllServersQuery();

            return await _queryDispatcher.Dispatch<GetAllServersQuery, List<ServerDto>>(cmd, cancellationToken);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateServer([FromBody] UpdateServerCommand serverCmd, CancellationToken cancellationToken)
        {
            var server = await _commandDispatcher.Dispatch<UpdateServerCommand, Server>(serverCmd, cancellationToken);

            return Ok(ServerDto.FromDomain(server));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateServer([FromBody] CreateServerCommand serverCmd, CancellationToken cancellationToken)
        {
            var server = await _commandDispatcher.Dispatch<CreateServerCommand, Server>(serverCmd, cancellationToken);

            return Ok(ServerDto.FromDomain(server));
        }

        [HttpPost("delete/{serverId}")]
        public async Task<IActionResult> DeleteServer(Guid serverId, CancellationToken cancellationToken)
        {
            var cmd = new DeleteServerCommand(serverId);
            await _commandDispatcher.Dispatch<DeleteServerCommand, NoResult>(cmd, cancellationToken);

            return Ok();
        }

        [HttpPost("secrets/add")]
        public async Task<IActionResult> CreateServerSecret([FromBody] CreateServerSecretCommand secretCmd, CancellationToken cancellationToken)
        {
            var secret = await _commandDispatcher.Dispatch<CreateServerSecretCommand, ServerSecret>(secretCmd, cancellationToken);
            return Ok(ServerSecretDto.FromDomain(secret));
        }

        [HttpPost("secrets/delete")]
        public async Task<IActionResult> DeleteServerSecret([FromBody] DeleteServerSecretCommand secretCmd, CancellationToken cancellationToken)
        {
            await _commandDispatcher.Dispatch<DeleteServerSecretCommand, NoResult>(secretCmd, cancellationToken);
            return Ok();
        }

        [HttpPost("secrets/update")]
        public async Task<IActionResult> UpdateServerSecret([FromBody] UpdateServerSecretCommand secretCmd, CancellationToken cancellationToken)
        {
            var secret = await _commandDispatcher.Dispatch<UpdateServerSecretCommand, ServerSecret>(secretCmd, cancellationToken);
            return Ok(ServerSecretDto.FromDomain(secret));
        }
    }
}
