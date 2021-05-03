namespace LFM.Domain.Write.Models
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