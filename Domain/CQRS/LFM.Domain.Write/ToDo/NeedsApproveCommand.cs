using LFM.Domain.Write.Declarations;
using Newtonsoft.Json;

namespace LFM.Domain.Write.ToDo
{
    public abstract class NeedsApproveCommand : ICommand
    {
        [JsonIgnore]
        public abstract string OperationUniqueKey { get; }
        public abstract ToDoOperationsEnum Operation { get; }
    }
}