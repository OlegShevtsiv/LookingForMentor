namespace LFM.Domain.Write.ResultModels
{
    public class IdCommandResult : CommandResult
    {
        public IdCommandResult(bool isSuccess) : base(isSuccess)
        {
        }

        public int Id { get; set; }
    }
}