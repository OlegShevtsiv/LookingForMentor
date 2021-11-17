using AutoMapper;
using LFM.DataAccess.DB.Core.Entities.ToDoEntities;
using Lfm.Domain.Manager.Models.ReviewModels;

namespace Lfm.Domain.Manager.Services.Mapper
{
    internal class ManagersModelsMaps : Profile
    {
        public ManagersModelsMaps()
        {
            CreateToDoModelsMaps();
        }

        void CreateToDoModelsMaps()
        {
            CreateMap<ToDoEntity, PendingToDoReviewModel>()
                .ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
                .ForMember(t => t.OperationCode, source => source.MapFrom(s => s.Operation.Code))
                .ForMember(t => t.CreatedByUser, source => source.MapFrom(s => s.CreatedByUser.UserName))
                .ForMember(t => t.CreatedDateTime, source => source.MapFrom(s => s.CreatedDateTime));

        }
    }
}