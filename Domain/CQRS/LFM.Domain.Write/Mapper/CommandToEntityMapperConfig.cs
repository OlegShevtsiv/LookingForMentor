using System;
using AutoMapper;
using LFM.DataAccess.DB.Core.Entities;
using LFM.DataAccess.DB.Core.Entities.MentorEntities;
using LFM.Domain.Write.Commands.Order;

namespace LFM.Domain.Write.Mapper
{
    internal class CommandToEntityMapperConfig : Profile
    {
        public CommandToEntityMapperConfig()
        {
            CreateOrdersMaps();
        }

        private void CreateOrdersMaps()
        {
            CreateMap<CreatePersonalOrderToMentorCommand, OrderRequest>(MemberList.Destination)
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.CostFrom, o => o.Ignore())
                .ForMember(x => x.CostTo, o => o.Ignore())
                .ForMember(x => x.Mentor, o => o.Ignore())
                .ForMember(x => x.CreationDateTime, o => o.MapFrom(p => DateTime.UtcNow));

            CreateMap<OrderRequest, MentorsOrder>(MemberList.Destination)
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.CostPerHour, o => o.Ignore())
                .ForMember(x => x.ApprovedDateTime, o => o.MapFrom(p => DateTime.UtcNow))
                ;
        }
    }
}