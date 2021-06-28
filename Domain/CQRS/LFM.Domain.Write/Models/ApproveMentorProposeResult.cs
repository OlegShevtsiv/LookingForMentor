namespace LFM.Domain.Write.Models
{
    public class ApproveMentorProposeResult : IdCommandResult
    {
        public ApproveMentorProposeResult(bool isSuccess) : base(isSuccess)
        {
        }

        public string MentorName { get; set; }
    }
}