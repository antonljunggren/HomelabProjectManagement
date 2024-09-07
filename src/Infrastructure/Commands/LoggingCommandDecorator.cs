using Core.Common.Commands;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
    internal sealed class LoggingCommandDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        private readonly ICommandHandler<TCommand, TResult> _handler;
        private readonly ILogger<LoggingCommandDecorator<TCommand, TResult>> _logger;

        public LoggingCommandDecorator(ICommandHandler<TCommand, TResult> handler, ILogger<LoggingCommandDecorator<TCommand, TResult>> logger)
        {
            _handler = handler;
            _logger = logger;
        }

        public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var cmdType = typeof(TCommand).Name;

            try
            {
                var stopwatch = Stopwatch.StartNew();
                var result = await _handler.Handle(command, cancellationToken);
                stopwatch.Stop();
                _logger.LogInformation($"Executed command: {cmdType} in {stopwatch.ElapsedMilliseconds}ms");
                return result;
            }
            catch (Exception)
            {
                _logger.LogError($"Command: {cmdType} failed to execute!");
                throw;
            }
        }
    }
}
