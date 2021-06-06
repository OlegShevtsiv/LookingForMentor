using LFM.Domain.Write.Declarations;

namespace LFM.Domain.Write.Commands.MentorProfile
{
    public class ApprovePersonalOrderCommand : ICommand
    {
        public int MentorId { get; set; }

        public int OrderRequestId { get; set; }
    }
}