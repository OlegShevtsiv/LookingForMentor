using LFM.Domain.Write.Declarations;

namespace LFM.Domain.Write.ToDo
{
    public abstract class NeedsApproveCommand : ICommand
    {
        public abstract ToDoOperationsEnum Operation { get; }
    }
}