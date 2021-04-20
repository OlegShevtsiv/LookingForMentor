using System.Threading.Tasks;
using LFM.Core.Common.Command;

namespace LFM.Domain.Write.Mediator
{
    public interface ICommandBus
    {
        Task<TCommandResult> ExecuteCommand<TCommand, TCommandResult>(TCommand command)
            where TCommand : ICommand
            where TCommandResult : CommandResult;
    }
}