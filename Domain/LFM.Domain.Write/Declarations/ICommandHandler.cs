using System.Threading.Tasks;
using LFM.Domain.Write.Models;

namespace LFM.Domain.Write.Declarations
{
    public interface ICommandHandler<in TCommand, TCommandResult> 
        where TCommand : ICommand
        where TCommandResult : CommandResult
    {
        Task<TCommandResult> ExecuteAsync(TCommand command);
    }
}