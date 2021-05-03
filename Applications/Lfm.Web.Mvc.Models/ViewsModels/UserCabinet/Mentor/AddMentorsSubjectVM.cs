using System.Collections.Generic;

namespace Lfm.Web.Mvc.Models.ViewsModels.UserCabinet.Mentor
{
    public class AddMentorsSubjectVM
    {
        public int CostPerHour { get; set; }
            
        public int SubjectId { get; set; }
            
        public List<int> TagIds { get; set; }
    }
}