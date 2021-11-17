using System;

namespace Lfm.Domain.Manager.Models.ReviewModels
{
    public class PendingToDoReviewModel
    {
        public int Id { get; set; }
        
        public string OperationCode { get; set; }
        

        public string CreatedByUser { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}