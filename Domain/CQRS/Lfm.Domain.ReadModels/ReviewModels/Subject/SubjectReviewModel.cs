using System.Collections.Generic;

namespace Lfm.Domain.ReadModels.ReviewModels.Subject
{
    public class SubjectReviewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<TagReviewModel> Tags { get; set; }
    }
}