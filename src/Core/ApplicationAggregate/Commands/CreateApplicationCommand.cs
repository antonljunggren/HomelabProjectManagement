using Core.Common.Commands;

namespace Core.ApplicationAggregate.Commands
{
    public sealed record CreateApplicationCommand(string ApplicationName, Guid? ServerId, int Port, string CodeRepository) : ICommand<Application>
    {
    }
}
