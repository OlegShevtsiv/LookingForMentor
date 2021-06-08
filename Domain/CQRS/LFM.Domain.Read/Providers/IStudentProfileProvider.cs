using System.Collections.Generic;
using System.Threading.Tasks;
using Lfm.Domain.ReadModels.ReviewModels.StudentProfile;

namespace LFM.Domain.Read.Providers
{
    public interface IStudentProfileProvider
    {
        Task<ICollection<FindMentorRequestReviewModel>> GetFindMentorRequests(int studentId);
    }
}