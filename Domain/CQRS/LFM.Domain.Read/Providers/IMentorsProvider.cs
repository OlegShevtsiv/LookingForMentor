using System.Threading.Tasks;
using Lfm.Domain.ReadModels.Common;
using Lfm.Domain.ReadModels.ReviewModels.Mentor;
using Lfm.Domain.ReadModels.SearchModels;

namespace LFM.Domain.Read.Providers
{
    public interface IMentorsProvider
    {
        Task<PageModel<MentorPreviewModel>> LookingForMentors(MentorsMinSearchModel searchModel);

        Task<MentorDetailedPreviewModel> GetMentorInfo(int mentorId);

        Task<ContactMentorInfo> GetContactMentorInfo(int mentorId, int subjectId);
    }
}