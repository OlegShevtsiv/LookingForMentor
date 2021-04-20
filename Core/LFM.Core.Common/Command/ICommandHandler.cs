using System.Threading.Tasks;

namespace LFM.Core.Common.Command
{
    public interface ICommandHandler<in TCommand, TCommandResult> 
        where TCommand : ICommand
        where TCommandResult : CommandResult
    {
        Task<TCommandResult> ExecuteAsync(TCommand command);
    }
}