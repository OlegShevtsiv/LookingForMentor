using System.Threading.Tasks;
using LFM.Domain.Write.Declarations;
using LFM.Domain.Write.Models;

namespace LFM.Domain.Write.Mediator
{
    public interface ICommandBus
    {
        Task<TCommandResult> ExecuteCommand<TCommand, TCommandResult>(TCommand command)
            where TCommand : ICommand
            where TCommandResult : CommandResult;
    }
}