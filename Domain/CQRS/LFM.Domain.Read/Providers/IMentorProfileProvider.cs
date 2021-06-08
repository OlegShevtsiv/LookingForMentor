using System.Collections.Generic;
using System.Threading.Tasks;
using Lfm.Domain.ReadModels.ReviewModels.MentorProfile;
using Lfm.Domain.ReadModels.ReviewModels.Subject;

namespace LFM.Domain.Read.Providers
{
    public interface IMentorProfileProvider
    {
        Task<T> GetGeneralInfo<T>(int mentorId) where T : class;

        Task<ICollection<MentorSubjectReviewModel>> GetSubjectsInfo(int mentorId);

        Task<ICollection<SubjectListItem>> GetAvailableSubjects(int mentorId);

        Task<MentorSubjectReviewModel> GetSubject(int mentorId, int subjectId);

        Task<bool> CanAddSubject(int mentorId, int subjectId);
        
        Task<byte[]> GetAvatar(int mentorId);

        Task<ICollection<T>> GetPersonalOrdersRequests<T>(int mentorId) where T : MentorsOrderMinReviewModel;

        Task<MentorPersonalOrderDetailedReviewModel> GetPersonalOrderRequestDetails(int mentorId, int orderId);

        Task<ICollection<T>> GetMentorsOrders<T>(int mentorId) where T : MentorsOrderMinReviewModel;

        Task<MentorsOrderDetailedReviewModel> GetMentorsOrderDetails(int mentorId, int orderId);
    }
}