namespace LFM.Core.Common.Command
{
    public class CommandResult
    {
        public bool IsSuccess { get; }
        
        public CommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}