using System.Threading.Tasks;

namespace LFM.Domain.Read.Providers
{
    public interface IMentorProfileProvider
    {
        Task<T> GetGeneralInfo<T>(int mentorId) where T : class;

        Task<byte[]> GetAvatar(int mentorId);
    }
}