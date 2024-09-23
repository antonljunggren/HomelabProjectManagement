using Core.ApplicationAggregate;
using Core.ApplicationAggregate.Commands;
using Core.ApplicationAggregate.Queries;
using Core.Common.Commands;
using Core.Common.Query;
using Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ILogger<ApplicationController> _logger;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public ApplicationController(ILogger<ApplicationController> logger, IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public async Task<IEnumerable<ApplicationDTO>> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllApplicationsQuery();

            return await _queryDispatcher.Dispatch<GetAllApplicationsQuery, List<ApplicationDTO>>(query, cancellationToken);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateApplication(CreateApplicationCommand cmd, CancellationToken cancellationToken)
        {
            var application = await _commandDispatcher.Dispatch<CreateApplicationCommand, Application>(cmd, cancellationToken);
            return Ok(ApplicationDTO.FromDomain(application));
        }
    }
}
