namespace LFM.Domain.Write.Models
{
    public class CreatePersonalOrderResult : CommandResult
    {
        public CreatePersonalOrderResult(bool isSuccess) : base(isSuccess)
        {
        }

        public string MentorName { get; set; }
    }
}