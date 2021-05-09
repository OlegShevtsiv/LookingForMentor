using System.Linq;
using AutoMapper;
using LFM.DataAccess.DB.Core.Entities.MentorEntities;
using LFM.DataAccess.DB.Core.Entities.SubjectEntities;
using Lfm.Domain.ReadModels.ReviewModels.MentorProfile;
using Lfm.Domain.ReadModels.ReviewModels.Subject;

namespace LFM.Domain.Read.Mapper
{
    internal class ReadModelsMapperConfigs : Profile
    {
        public ReadModelsMapperConfigs()
        {
            CreateMentorProfileMaps();
            CreateSubjectMaps();
        }

        private void CreateMentorProfileMaps()
        {
            CreateMap<SubjectsTag, MentorProfilePreviewModel.TagInfo>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.Id))
                .ForMember(x => x.Name, o => o.MapFrom(p => p.Name));

            CreateMap<MentorsSubjectInfo, MentorProfilePreviewModel.SubjectInfo>()
                .ForMember(x => x.CostPerHour, o => o.MapFrom(p => p.CostPerHour))
                .ForMember(x => x.SubjectName, o => o.MapFrom(p => p.Subject.Name))
                .ForMember(x => x.Tags, o => o.MapFrom(p => p.Subject.Tags));

            CreateMap<MentorsProfile, MentorProfilePreviewModel>()
                .ForMember(x => x.Surname, o => o.MapFrom(p => p.Surname))
                .ForMember(x => x.MiddleName, o => o.MapFrom(p => p.MiddleName))
                .ForMember(x => x.ProfileImageId, o => o.MapFrom(p => p.ProfileImageId))
                .ForMember(x => x.AboutMe, o => o.MapFrom(p => p.AboutMe))
                .ForMember(x => x.SubjectsInfos, o => o.MapFrom(p => p.SubjectsInfo))
                .ForMember(x => x.TownName, o => o.MapFrom(p => p.Town.Name))
                .ForMember(x => x.StudyingPlace, o => o.MapFrom(p => p.StudyingPlace))
                .ForMember(x => x.Education, o => o.MapFrom(p => p.Education));
            
            CreateMap<MentorsSubjectInfo, MentorSubjectReviewModel>()
                .ForMember(x => x.CostPerHour, o => o.MapFrom(p => p.CostPerHour))
                .ForMember(x => x.Description, o => o.MapFrom(p => p.Description))
                .ForMember(x => x.SubjectId, o => o.MapFrom(p => p.SubjectId))
                .ForMember(x => x.SubjectName, o => o.MapFrom(p => p.Subject.Name))
                .ForMember(x => x.SelectedTags, o => o.MapFrom(p => p.Tags));

            CreateMap<MentorsSubjectTag, MentorSubjectReviewModel.MentorsSubjectTag>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.TagId))
                .ForMember(x => x.TagName, o => o.MapFrom(p => p.Tag.Name));
        }

        private void CreateSubjectMaps()
        {
            CreateMap<SubjectsTag, SubjectReviewModel.Tag>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.Id))
                .ForMember(x => x.Name, o => o.MapFrom(p => p.Name));
            
            CreateMap<Subject, SubjectReviewModel>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.Id))
                .ForMember(x => x.Name, o => o.MapFrom(p => p.Name))
                .ForMember(x => x.Tags, o => o.MapFrom(p => p.Tags));

            CreateMap<SubjectReviewModel, SubjectListItem>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.Id))
                .ForMember(x => x.Name, o => o.MapFrom(p => p.Name));
        }
    }
}