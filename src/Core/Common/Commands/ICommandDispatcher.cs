
namespace Core.Common.Commands
{
    public interface ICommandDispatcher
    {
        Task<TResult> Dispatch<TCommand, TResult>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand<TResult>;
    }
}
