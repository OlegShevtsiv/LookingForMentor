namespace LFM.Domain.Write.Models
{
    public class IdCommandResult : CommandResult
    {
        public IdCommandResult(bool isSuccess) : base(isSuccess)
        {
        }

        public int Id { get; set; }
    }
}