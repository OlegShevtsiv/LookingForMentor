using System.Threading.Tasks;
using LFM.Domain.Write.Declarations;
using LFM.Domain.Write.ResultModels;
using LFM.Domain.Write.ToDo;

namespace LFM.Domain.Write.Mediator
{
    public interface IMediator
    {
        Task<TCommandResult> ExecuteCommand<TCommand, TCommandResult>(TCommand command)
            where TCommand : ICommand, new()
            where TCommandResult : CommandResult;

        Task CreateToDo<TCommand>(TCommand command)
            where TCommand : NeedsApproveCommand, new();
    }
}