
namespace Core.Common.Commands
{
    public interface ICommand<TResult>
    {
    }

    public interface ICommand : ICommand<NoResult>
    {
    }

    public struct NoResult
    {

    }
}
