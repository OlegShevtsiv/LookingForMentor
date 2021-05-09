using System.Collections.Generic;
using System.Threading.Tasks;
using Lfm.Domain.ReadModels.ReviewModels.Subject;

namespace LFM.Domain.Read.Providers
{
    public interface ISubjectsProvider
    {
        public Task<IEnumerable<SubjectReviewModel>> GetAllSubjects();

        public Task<IEnumerable<SubjectListItem>> GetSubjectsList();
        
        Task<SubjectReviewModel> GetSubject(int subjectId);

        Task<bool> IsExists(int subjectId);
    }
}