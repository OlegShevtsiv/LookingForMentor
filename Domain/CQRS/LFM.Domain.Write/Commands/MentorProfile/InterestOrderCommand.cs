using LFM.Domain.Write.Declarations;

namespace LFM.Domain.Write.Commands.MentorProfile
{
    public class InterestOrderCommand : ICommand
    {
        public int OrderId { get; set; }

        public int MentorId { get; set; }
    }
}