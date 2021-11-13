namespace LFM.Domain.Write.ResultModels
{
    public class CreatePersonalOrderResult : CommandResult
    {
        public CreatePersonalOrderResult(bool isSuccess) : base(isSuccess)
        {
        }

        public string MentorName { get; set; }
    }
}