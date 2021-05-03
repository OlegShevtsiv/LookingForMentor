using System.Collections.Generic;

namespace Lfm.Domain.ReadModels.ReviewModels.Subject
{
    public class SubjectReviewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Tag> Tags { get; set; }
        
        public class Tag
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}